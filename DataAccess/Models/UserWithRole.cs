using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Models
{
    public class UserWithRole
    {
        public Guid UserId { get; set; }
        public string GivenRole { get; set; }
    }
}
