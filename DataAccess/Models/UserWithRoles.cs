using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public class UserWithRoles
    {
        public Guid Id { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}
