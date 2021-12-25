using Accounting.Shared.ViewModels.AccountViewModels;
using Accounting.WebAPI.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using AutoMapper;

namespace Accounting.WebAPI.Services
{
    public class AuthManager : IAuthManager
    {
        private readonly UserManager<ApiUser> _userManager;
        private readonly IConfiguration _configuration;
        private ApiUser _user;
        private readonly IMapper _mapper;

        public AuthManager(UserManager<ApiUser> userManager,
             IMapper mapper,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<string> CreateToken(LoginUserDTO userDTO)
        {
            var signingCredentials = GetSigningCredentials();
            var claims = await GetClaims(userDTO);
            var token = GenerateTokenOptions(signingCredentials, claims);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<bool> ValidateUser(LoginUserDTO userDTO)
        {
            var _user = await _userManager.FindByNameAsync(userDTO.Email);
            var validPassword = await _userManager.CheckPasswordAsync(_user, userDTO.PassWord);
            return (_user != null && validPassword);
        }

        private SigningCredentials GetSigningCredentials()
        {
            var key = Environment.GetEnvironmentVariable("Key");
            var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private async Task<List<Claim>> GetClaims(LoginUserDTO userDTO)
        {
            var user = _mapper.Map<ApiUser>(userDTO);
            user.UserName = userDTO.Email;
            var userResult = await _userManager.FindByNameAsync(user.UserName);
            var claims = new List<Claim>
             {
                 new Claim(ClaimTypes.Name, userResult.UserName)
             };

            var roles = await _userManager.GetRolesAsync(userResult);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var jwtSettings = _configuration.GetSection("Jwt");
            var expiration = DateTime.Now.AddMinutes(Convert.ToDouble(
                jwtSettings.GetSection("TokenExpiresInMinutes").Value));

            var token = new JwtSecurityToken(
                issuer: jwtSettings.GetSection("Issuer").Value,
                audience: jwtSettings.GetSection("validAudience").Value,
                claims: claims,
                expires: expiration,
                signingCredentials: signingCredentials
                );

            return token;
        }
    }
}
