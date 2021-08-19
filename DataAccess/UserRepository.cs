using BusinessLogic.Models;
using DataAccess.Classes;
using DataAccess.Interfaces;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess
{
    public class UserRepository : IUserRepository
    {
        private readonly DbContextOptions<UsersDBContext> _options;
        private readonly IRolesRepository _rolesRepository;
        
        public UserRepository(DbContextOptions<UsersDBContext> options, IRolesRepository rolesRepository)
        {
            _options = options;
            _rolesRepository = rolesRepository;
        }

        public async Task<Guid> AddUser(UserDTO user)
        {
            using (var context = new UsersDBContext(_options))
            {
                try
                {
                    await context.AddAsync(user);
                    await context.SaveChangesAsync();
                    return user.Id;
                }
                catch (Exception)
                {
                    return Guid.Empty;
                }

            }
        }

        public async Task<UserDTO> GetUserById(Guid id)
        {
            using (var context = new UsersDBContext(_options))
            {
                try
                {
                    var user = await context.Users.FirstOrDefaultAsync(x => x.Id == id);
                    return user;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public IEnumerable<UserDTO> GetUsers()
        {
            using (var context = new UsersDBContext(_options))
            {
                try
                {
                    var users = context.Users.ToList();
                    return users;
                }
#pragma warning disable CS0168 // Variable is declared but never used
                catch (Exception ex)
#pragma warning restore CS0168 // Variable is declared but never used
                {
                    return null;
                }
            }
        }

        public async Task<bool> PutUser(UserDTO user)
        {
            using (var context = new UsersDBContext(_options))
            {
                try
                {
                    var foundUser = await context.Users.FirstOrDefaultAsync(x => x.Id == user.Id);
                    context.Update(user);
                    await context.SaveChangesAsync();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public async Task<bool> DeleteUserById(Guid id)
        {
            using (var context = new UsersDBContext(_options))
            {
                try
                {
                    var userToDelete = await context.Users.FirstOrDefaultAsync(x => x.Id == id);
                    context.Users.Remove(userToDelete);
                    await context.SaveChangesAsync();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        

        public async Task<UserDTO> GetUserByAuthData(AuthenticationModel authenticationModel)
        {
            using (var context = new UsersDBContext(_options))
            {
                var foundUser = await context.Users.FirstOrDefaultAsync(x => x.Nickname == authenticationModel.Login &&
                    x.PasswordHash == authenticationModel.Password);
                return foundUser ?? throw new ArgumentNullException("User not found!");
            }
        }

        public async Task<bool> RegisterUser(UserDTO userToRegister, string starterRole)
        {
            bool resultAddingUser = false;
            bool resultAddingRole = false;
            using (var context = new UsersDBContext(_options))
            {
                try
                {
                    resultAddingUser = await context.Users.AddAsync(userToRegister) != null;
                    await context.SaveChangesAsync();
                }
                catch(Exception ex)
                {
                    return false;
                }
            }

            resultAddingRole = await AddStarterRole(userToRegister.Id, starterRole);

            return resultAddingRole && resultAddingUser;
        }

        private async Task<bool> AddStarterRole(Guid id, string starterRoleName)
        {
            return await _rolesRepository.SetRole(id, starterRoleName);
        }
    }
}
