using System;

namespace BusinessLogic.Models
{
    public class UserRoles
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
    }
}
