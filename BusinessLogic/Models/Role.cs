using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Models
{
    public class Role
    {
        public Guid Id { get; set; }
        public string RoleName { get; set; }
        public Role() { }
        public Role(string role)
        {
            Id = Guid.NewGuid();
            RoleName = role;
        }

        public override string ToString()
        {
            return $"{Id} - {RoleName}";
        }
    }
}
