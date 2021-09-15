using DataAccess.Models;
using System.Collections.Generic;

namespace BusinessLogic.Models
{
    public class ValidationResult
    {
        public bool IsSuccessful { get; set; }
        public UserWithRoles UserWithRoles { get; set; }
        public Dictionary<string, string> AdditionalParams { get; set; }

        public ValidationResult(bool isSuccessful, UserWithRoles userWithRole)
        {
            IsSuccessful = isSuccessful;
            UserWithRoles = userWithRole;
            AdditionalParams = new Dictionary<string, string>();
        }
    }
}
