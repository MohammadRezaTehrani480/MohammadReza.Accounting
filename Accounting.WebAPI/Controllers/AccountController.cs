using Accounting.Shared.ViewModels.AccountViewModels;
using Accounting.WebAPI.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Accounting.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        /*Notice the context for user manager and sign in manager is Api user or whatever a custome user class that you have used when you were
        setting up the identity.User manager gives us access to bunch of funtionalities that allow us to manage sign in, retrieve user information so we do not need 
        anu unit of work fore users table intraction or role table intraction all these things will come out of the box with user manager
        role manager and sign-in manger*/

        private readonly UserManager<ApiUser> _userManager;
        private readonly SignInManager<ApiUser> _signInManager;
        private readonly ILogger<AccountController> _logger;
        private readonly IMapper _mapper;

        public AccountController(UserManager<ApiUser> userManager,
            SignInManager<ApiUser> signInManager,
            ILogger<AccountController> logger,
            IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _mapper = mapper;
        }

        /*Because we have two post verbs they may interfear with each other so we have to add
         route attribute to each action method and incoming reiquest would be like
         api/account/register or api/account/login*/

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] UserDTO userDTO)
        {
            _logger.LogInformation($"Registration Attemp for {userDTO.Email}");

            if (!ModelState.IsValid)
            {
                /*That means you have sent over a request to register but your validation faied
                 so you did not include email or you did not meet whatever standards I would have laid out for you
                regarding a data you should have sent so I can return the modelState to inform the sender that what
                went wrong*/
                return BadRequest(ModelState);
            }

            try
            {
                var user = _mapper.Map<ApiUser>(userDTO);
                /*It will automatically take it , take the password , hash it , store it and do everything that needs to
                have done*/
                var result = await _userManager.CreateAsync(user);

                if (!result.Succeeded)
                {
                    return BadRequest("$User Registration Attep faied!");
                }

                return Accepted();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(Register)}");
                return Problem($"Something went wrong in the {nameof(Register)}", statusCode: 500);
            }
        }


        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDTO userDTO)
        {
            _logger.LogInformation($"Login Attemp for {userDTO.Email}");

            if (!ModelState.IsValid)
            {
                /*That means you have sent over a request to register but your validation faied*/
                return BadRequest(ModelState);
            }

            try
            {
                /*This an API , so we do noy know what application would call that for example it could be postman
                  it could be the browser or could be a mobile app so we do not need to persist anything    */
                var result = await _signInManager.PasswordSignInAsync(userDTO.Email, userDTO.PassWord, false, false);

                if (!result.Succeeded)
                {
                    return Unauthorized(userDTO);
                }

                return Accepted();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(Login)}");
                return Problem($"Something went wrong in the {nameof(Login)}", statusCode: 500);
            }
        }
    }
}