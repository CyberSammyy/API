using BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Interfaces
{
    public interface IAuthService
    {
        ValidationResult Login(AuthenticationModel authenticationModel);
        bool RegisterUser(User userToRegister);
        bool ConfirmEmail(string message);
    }
}
