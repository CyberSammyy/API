using BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface IUserService
    {
        public Task<Guid> AddUser(User user);
        public Task<bool> DeleteUserById(Guid id);
        public Task<User> GetUserById(Guid id);
        public IEnumerable<User> GetUsers();
        public Task<bool> PutUser(User user);
        public Task<IEnumerable<string>> GetUserRolesById(Guid id);
        public Task<User> GetUserByLoginAndPassword(AuthenticationModel userAuthData);
        public Task<bool> RegisterUser(User userToRegister);
    }
}
