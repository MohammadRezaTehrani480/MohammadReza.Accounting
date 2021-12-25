using Accounting.Shared.ViewModels.AccountViewModels;
using Accounting.WebAPI.Entities;
using Accounting.WebAPI.Services;
using AutoMapper;
using Microsoft.AspNetCore.Http;
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
        private readonly ILogger<AccountController> _logger;
        private readonly IMapper _mapper;
        private readonly IAuthManager _authManager;

        public AccountController(UserManager<ApiUser> userManager,
            ILogger<AccountController> logger,
            IMapper mapper,
            IAuthManager authManager)
        {
            _userManager = userManager;
            _logger = logger;
            _mapper = mapper;
            _authManager = authManager;
        }

        /*Because we have two post verbs they may interfear with each other so we have to add
         route attribute to each action method and incoming reiquest would be like
         api/account/register or api/account/login*/
        /*A login realy is to creat session and allow somebody to access the system for period whereas with an apiwe do not
         know what period of time you may need the access so we do not need to facilitate you for a longer than it takes to proccess
         your request and give you your respose so we just want to know that you are accessible acccesser , validate that give you what you 
         want that is why we are going to use token*/
        [HttpPost]
        [Route("register")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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

            var user = _mapper.Map<ApiUser>(userDTO);
            /*ما مپینگ رو انجام دادیم ولی میخواهیم بگوویم ایمیل را همان فیلد یوزر نیم هست که به صورت دیفالت 
             در کلاس ای پی آی یوزر قرار دارد*/
            user.UserName = userDTO.Email;
            /*It will automatically take it , take the password , hash it , store it and do everything that needs to
            have done*/
            /*By adding userDTO.PassWord in Create Async method our password will arore in database*/
            var result = await _userManager.CreateAsync(user, userDTO.PassWord);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
                return BadRequest("$User Registration Attep faied!");
            }
            await _userManager.AddToRolesAsync(user, userDTO.Roles);
            return Accepted();
        }

        /*When a person tries to login to access our api we are going to tell them who you are and what
         you are able to do but then we need to make sure that you are somebody who should be able to 
         access my system to begin with. they would pass over their username and passwordthat we have already 
         stored in our system because they have registered , one we verify that are who they say they are we are 
         going to give them a token with information that we have on system so when they are making subsquence requests
         they can use this token instead of trying to login every singel time.The benefit of this is that on their side
        they do not need to login every singel time. Remeber thatwe are not keeping a session to know if they loged in
        previously or not.They will login once get this token and then make every other call with this token attached
        to the request.
        We have three parts in encoded jwt token. firt part is header which consists algorithem which contains what hashing alg
        was used and the the type of the token which is jwt
        The middle section has the payload or the data so payload is the all info that we know about this user
        as long as this token is valid then they do not have to login again as soon as it expires and become invalid they may want
        to asked to login again and get fresh token and the continue*/

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDTO userDTO)
        {
            _logger.LogInformation($"Login Attempt for {userDTO.Email} ");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await _authManager.ValidateUser(userDTO))
            {
                return Unauthorized();
            }

            return Accepted(new { Token = await _authManager.CreateToken(userDTO) });
        }
    }
}