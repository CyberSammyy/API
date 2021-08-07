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
    public class RolesRepository : IRolesRepository
    {
        private readonly DbContextOptions<UsersDBContext> _options;

        public RolesRepository(DbContextOptions<UsersDBContext> options)
        {
            _options = options;
        }

        public async Task<IList<RoleDTO>> GetRolesById(Guid id)
        {
            IList<RoleDTO> result = new List<RoleDTO>();
            using (var context = new UsersDBContext(_options))
            {
                var userRoles = await context.UserRoles.Where(x => x.UserId == id).ToListAsync();
                foreach(var userRole in userRoles)
                {
                    result.Add(await context.Roles.FirstOrDefaultAsync(x => x.Id == userRole.RoleId));
                }
                return result;
            }
        }

        public async Task<bool> RemoveRole(Guid userId, string roleName)
        {
            using (var context = new UsersDBContext(_options))
            {
                var isUserWithRoles = context.UserRoles.Where(x => x.UserId == userId).Count() > 0;
                var userRoles = await context.UserRoles.ToListAsync();
                if(!isUserWithRoles)
                {
                    return false;
                }
                try
                {
                    foreach(var userRole in userRoles)
                    {
                        var existingRole = await context.Roles.FirstOrDefaultAsync(x => x.RoleName == roleName);
                        if(userRole.RoleId == existingRole.Id && userRole.UserId == userId)
                        {
                            context.UserRoles.Remove(userRole);
                        }
                    }
                }
#pragma warning disable CS0168 // Variable is declared but never used
                catch(Exception ex)
#pragma warning restore CS0168 // Variable is declared but never used
                {
                    return false;
                }
                await context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<bool> SetRole(Guid userId, string roleName)
        {
            using (var context = new UsersDBContext(_options))
            {
                var userRoles = await context.UserRoles.Where(x => x.UserId == userId).ToListAsync();
                try
                {
                    foreach (var userRole in userRoles)
                    {
                        var existingRole = await context.Roles
                            .FirstOrDefaultAsync(x => x.RoleName == roleName && x.Id == userRole.RoleId);
                        if (existingRole != null)
                        {
                            return false;
                        }
                    }
                }
#pragma warning disable CS0168 // Variable is declared but never used
                catch(Exception ex)
#pragma warning restore CS0168 // Variable is declared but never used
                {
                    return false;
                }
                var roleToAdd = await context.Roles.Where(x => x.RoleName == roleName).FirstOrDefaultAsync();
                if(roleToAdd == null)
                {
                    return false;
                }
                await context.UserRoles.AddAsync(new UserRolesDTO
                {
                    UserId = userId,
                    Id = Guid.NewGuid(),
                    RoleId = roleToAdd.Id
                });
                await context.SaveChangesAsync();
                return true;
            }
        }
    }
}
