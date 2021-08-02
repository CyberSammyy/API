using AutoMapper;
using BusinessLogic.Classes;
using BusinessLogic.Interfaces;
using BusinessLogic.Models;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace BusinessLogic.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserService _userService;
        private readonly IHashService _hashService;
        private readonly IMapper _mapper;

        public AuthService(IUserService userService, IMapper mapper, IHashService hashService)
        {
            _userService = userService;
            _hashService = hashService;
            _mapper = mapper;
        }

        public bool ConfirmEmail(string message)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Login(AuthenticationModel authenticationModel)
        {
            var hashedPassword = authenticationModel.Password.GetMD5Hash();
            authenticationModel.Password = hashedPassword;

            var foundUser = _userService.GetUserByLoginAndPassword(authenticationModel);
            UserWithRoles userWithRoles = null;
            var isUserNotNull = foundUser != null;

            if(isUserNotNull)
            {
                var roles = _userService.GetUserRolesById(foundUser.Id);
                userWithRoles = new UserWithRoles
                {
                    Id = foundUser.Id,
                    Roles = roles
                };
            }

            return new ValidationResult(isUserNotNull, userWithRoles);
        }

        public bool RegisterUser(User userToRegister)
        {
            throw new NotImplementedException();
        }
        private bool IsPasswordValid(string password)
        {
            var regEx = new Regex(@"^(?=.*?[A-Z])(?=.*?[a-z])((?=.*?[0-9])|(?=.*?[#?!@$%^&*-]))", RegexOptions.Compiled);

            return regEx.IsMatch(password);
        }
    }
}
