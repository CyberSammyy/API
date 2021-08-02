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
        private IEnumerable<UserWithRoles> _usersWithRoles;
        public UserRepository(DbContextOptions<UsersDBContext> options)
        {
            _options = options;
        }

        public async Task<IEnumerable<string>> GetUserRolesById(Guid id)
        {
            if(_usersWithRoles == null)
            {
                _usersWithRoles = await GetAllUserRoles();
            }

            return _usersWithRoles
                .FirstOrDefault(x => x.Id == id)?.Roles;
        }

        private async Task<IEnumerable<UserWithRoles>> GetAllUserRoles()
        {
            using (var context = new UsersDBContext(_options))
            {
                var items = await (from user in context.Set<UserDTO>()
                                   join userRole in context.Set<UserRolesDTO>()
                                   on user.Id equals userRole.UserId
                                   join role in context.Set<RoleDTO>()
                                   on userRole.RoleId equals role.Id
                                   select new UserWithRole
                                   {
                                       GivenRole = role.RoleName,
                                       UserId = user.Id
                                   }).ToListAsync();
                return items.GroupBy(x => x.UserId)
                    .Select(x => new UserWithRoles
                    {
                        Id = x.Key,
                        Roles = x.ToList().Select(x => x.GivenRole)
                    });
            }
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

        async Task<IEnumerable<string>> IUserRepository.GetUserRolesById(Guid userId)
        {
            using (var context = new UsersDBContext(_options))
            {
                var usersRoles = await context.UserRoles.Where(x => x.UserId == userId).ToListAsync();
                List<string> rolesStrings = new List<string>();
                foreach(var userRole in usersRoles)
                {
                    rolesStrings.Add((await context.Roles
                        .FirstOrDefaultAsync(x => x.Id == userRole.RoleId)).RoleName);
                }

                return rolesStrings;
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

        public bool RegisterUser(UserDTO userToRegister)
        {
            throw new NotImplementedException();
        }
    }
}
