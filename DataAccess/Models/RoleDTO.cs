using System;

namespace DataAccess.Models
{
    public class RoleDTO
    {
        public Guid Id { get; set; }
        public string RoleName { get; set; }

        public RoleDTO() { }

        public RoleDTO(Guid id, string role)
        {
            Id = id;
            RoleName = role;
        }

        public override string ToString()
        {
            return $"{Id} - {RoleName}";
        }
    }
}
