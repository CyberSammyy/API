using BusinessLogic.Interfaces;
using BusinessLogic.Models;
using BusinessLogic.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _service;
        private readonly AuthService _authService;
        private readonly ISessionService _sessionService;

        public UserController(ILogger<UserController> logger, IUserService service,
            AuthService authService, ISessionService sessionService)
        {
            _logger = logger;
            _service = service;
            _authService = authService;
            _sessionService = sessionService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public string Login(AuthenticationModel userAuthData)
        {
            var validationResult = _authService.Login(userAuthData);

            if(!validationResult.IsSuccessful)
            {
                BadRequest("Incorrect user authorization data!");
            }

            var token = _sessionService.CreateAuthToken(validationResult.UserWithRoles);
            
            return token;
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

        [AllowAnonymous]
        [HttpPost]
        public async Task<Guid> PostUser(User user)
        {
            return await _service.AddUser(user);
        }


        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<bool> DeleteById(Guid id)
        {
            return await _service.DeleteUserById(id);
        }

        [Authorize(Roles = "Moderator, Admin")]
        [HttpPut]
        public async Task<bool> PutUser(User user)
        {
            return await _service.PutUser(user);
        }
    }
}
