using AutoMapper;
using BusinessLogic.Interfaces;
using BusinessLogic.Models;
using DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class RolesService : IRolesService
    {
        private readonly IRolesRepository _rolesRepository;

        private readonly IMapper _mapper;

        public RolesService(IRolesRepository rolesRepository, IMapper mapper)
        {
            _rolesRepository = rolesRepository;
            _mapper = mapper;
        }

        public async Task<IList<Role>> GetRolesById(Guid id)
        {
            return _mapper.Map<IList<Role>>(await _rolesRepository.GetRolesById(id));
        }

        public async Task<bool> RemoveRole(Guid userId, string roleName)
        {
            return await _rolesRepository.RemoveRole(userId, roleName);
        }
        public async Task<IEnumerable<string>> GetUserRolesById(Guid id)
        {
            return await _rolesRepository.GetUserRolesById(id);
        }
        public async Task<bool> SetRole(Guid userId, string roleName)
        {
            return await _rolesRepository.SetRole(userId, roleName);
        }
    }
}
