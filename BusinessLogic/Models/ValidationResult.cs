using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Models
{
    public class ValidationResult
    {
        public bool IsSuccessful { get; set; }
        public UserWithRoles UserWithRoles { get; set; }

        public ValidationResult(bool isSuccessful, UserWithRoles userWithRole)
        {
            IsSuccessful = isSuccessful;
            UserWithRoles = userWithRole;
        }
    }
}
