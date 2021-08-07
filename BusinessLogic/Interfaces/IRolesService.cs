using BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface IRolesService
    {
        Task<IList<Role>> GetRolesById(Guid id);
        Task<bool> SetRole(Guid userId, string roleName);
        Task<bool> RemoveRole(Guid userId, string roleName);

    }
}
