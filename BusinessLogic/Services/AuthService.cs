using AutoMapper;
using BusinessLogic.Classes;
using BusinessLogic.Interfaces;
using BusinessLogic.Models;
using DataAccess.Models;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserService _userService;

        private readonly IHashService _hashService;

        private readonly IRolesService _rolesService;

        private readonly IMapper _mapper;

        public AuthService(IUserService userService, IMapper mapper, IHashService hashService, IRolesService rolesService)
        {
            _userService = userService;
            _hashService = hashService;
            _mapper = mapper;
            _rolesService = rolesService;
        }

        public async Task<ConfirmationResult> ConfirmEmail(string message)
        {
            try
            {
                return await _userService.ConfirmEmail(message);
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // Variable is declared but never used
            {
                return new ConfirmationResult
                {
                    IsSuccessful = false,
                    UserId = Guid.Empty
                };
            }
        }

        public async Task<ValidationResult> Login(AuthenticationModel authenticationModel)
        {
            var hashedPassword = authenticationModel.Password.GetMD5Hash();
            authenticationModel.Password = hashedPassword;

            var foundUser = await _userService.GetUserByLoginAndPassword(authenticationModel);
            UserWithRoles userWithRoles = null;
            var isUserNotNull = foundUser != null;

            if(isUserNotNull)
            {
                var roles = await _rolesService.GetUserRolesById(foundUser.Id);
                userWithRoles = new UserWithRoles
                {
                    Id = foundUser.Id,
                    Roles = roles
                };
            }

            return new ValidationResult(isUserNotNull, userWithRoles);
        }

        public async Task<bool> RegisterUser(User userToRegister, string path)
        {
            if(!IsPasswordValid(userToRegister.Password) || 
               !IsMailFormatValid(userToRegister.Email))
            {
                //return false;
                //return true;
            }

            var registrationResult = await _userService.RegisterUser(userToRegister);

            if (registrationResult)
            {
                _userService.AddUserMail(userToRegister.Id, userToRegister.Email, path);
            }

            return registrationResult;
        }

        private bool IsPasswordValid(string password)
        {
            var regEx = new Regex(@"^(?=.*?[A-Z])(?=.*?[a-z])((?=.*?[0-9])|(?=.*?[#?!@$%^&*-]))", RegexOptions.Compiled);

            return regEx.IsMatch(password);
        }

        private bool IsMailFormatValid(string email)
        {
            var regEx = new Regex(@"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*@((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$", 
                RegexOptions.Compiled);

            return regEx.IsMatch(email);
        }
    }
}
