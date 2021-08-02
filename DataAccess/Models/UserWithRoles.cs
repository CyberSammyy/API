using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Models
{
    public class UserWithRoles
    {
        public Guid Id { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}
