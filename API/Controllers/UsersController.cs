using BusinessLogic.Interfaces;
using BusinessLogic.Models;
using DistributedModels;
using HelperClasses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;

        private readonly IUserService _service;

        private readonly IAuthService _authService;

        private readonly ISessionService _sessionService;

        public UsersController(ILogger<UsersController> logger, IUserService service,
            IAuthService authService, ISessionService sessionService)
        {
            _logger = logger;
            _service = service;
            _authService = authService;
            _sessionService = sessionService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<string> Login(AuthenticationModel userAuthData)
        {
            var validationResult = await _authService.Login(userAuthData);

            if(!validationResult.IsSuccessful)
            {
                BadRequest("Incorrect user authorization data!");
            }

            var token = _sessionService.CreateAuthToken(validationResult.UserWithRoles);
            var id = validationResult.UserWithRoles.Id;
            var email = string.Empty;
            var phoneNumber = string.Empty;

            if(!validationResult.AdditionalParams.TryGetValue("Email", out email) ||
               !validationResult.AdditionalParams.TryGetValue("PhoneNumber", out phoneNumber))
            {
                BadRequest("Some data is missing!");
            }
            
            return token + HelperConstants.UNIVERSAL_RESPONSE_STRING_SEPARATOR + id //TODO Investigate more convinient solution how to pass email id and phone number
                + HelperConstants.UNIVERSAL_RESPONSE_STRING_SEPARATOR + email
                + HelperConstants.UNIVERSAL_RESPONSE_STRING_SEPARATOR + phoneNumber;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(User userToRegister)
        {
            var isRegistered = await _authService.RegisterUser(userToRegister, $"{Request.Scheme}://{Request.Host}");

            if(isRegistered)
            {
                return Ok(await Login(new AuthenticationModel
                {
                    Login = userToRegister.Nickname,
                    Password = userToRegister.Password
                }));
            }

            return BadRequest("Invalid registration data!");
        }

        [AllowAnonymous]
        [HttpGet]
        public IEnumerable<User> GetUsers()
        {
            return _service.GetUsers();
        }

        [Authorize(Roles = "Moderator, Admin")]
        [HttpGet("{id}")]
        public async Task<User> GetUserById(Guid id)
        {
            return await _service.GetUserById(id);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<bool> DeleteById(Guid id)
        {
            return await _service.DeleteUserById(id);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<bool> PutUser(User user)
        {
            return await _service.PutUser(user);
        }

        [Authorize]
        [HttpPut("changePassword")]
        public async Task<bool> ChangePassword(StringUserFieldChangeModel passwordToUpdate)
        {
            return await _service.ChangePassword(passwordToUpdate.UserId, passwordToUpdate.FieldToUpdate);
        }

        [Authorize]
        [HttpPut("changeNickname")]
        public async Task<bool> ChangeNickname(StringUserFieldChangeModel nicknameToUpdate)
        {
            return await _service.ChangeNickname(nicknameToUpdate.UserId, nicknameToUpdate.FieldToUpdate);
        }

        [AllowAnonymous]
        [HttpGet("confirm")]
        public async Task<IActionResult> Confirm(string message)
        {
            try
            {
                var result = await _authService.ConfirmEmail(message);

                return Ok(result);
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch(Exception ex)
#pragma warning restore CS0168 // Variable is declared but never used
            {
                return BadRequest();
            }
        }
    }
}
