using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Models
{
    public class UserRolesDTO
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
    }
}
