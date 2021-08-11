using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IRolesRepository
    {
        Task<IList<RoleDTO>> GetRolesById(Guid id);
        Task<bool> SetRole(Guid userId, string roleName);
        Task<bool> RemoveRole(Guid userId, string roleName);
        Task<IEnumerable<string>> GetUserRolesById(Guid userId);
    }
}
