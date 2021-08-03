using BusinessLogic.Models;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IUserRepository
    {
        public Task<Guid> AddUser(UserDTO user);
        public Task<bool> DeleteUserById(Guid id);
        public Task<bool> PutUser(UserDTO user);
        public Task<UserDTO> GetUserById(Guid id);
        public IEnumerable<UserDTO> GetUsers();
        Task<IEnumerable<string>> GetUserRolesById(Guid userId);
        Task<UserDTO> GetUserByAuthData(AuthenticationModel authenticationModel);
        Task<bool> RegisterUser(UserDTO userToRegister);

    }
}
