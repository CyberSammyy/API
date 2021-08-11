using BusinessLogic.Interfaces;
using BusinessLogic.Models;
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
    public class RolesController
    {
        private readonly ILogger<UserController> _logger;

        private readonly IRolesService _rolesService;

        public RolesController(ILogger<UserController> logger, IRolesService rolesService)
        {
            _logger = logger;
            _rolesService = rolesService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Moderator, Writer")]
        public async Task<IList<Role>> GetRolesById(Guid id)
        {
            return await _rolesService.GetRolesById(id);
        }

        [HttpPost("setUserRole")]
        [Authorize(Roles = "Admin")]
        public async Task<bool> SetRole(Guid userId, string roleName)
        {
            return await _rolesService.SetRole(userId, roleName);
        }

        [HttpDelete("removeUserRole")]
        [Authorize(Roles = "Admin")]
        public async Task<bool> RemoveRole(Guid userId, string roleName)
        {
            return await _rolesService.RemoveRole(userId, roleName);
        }
    }
}
