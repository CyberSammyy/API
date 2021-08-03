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

        public async Task<ValidationResult> Login(AuthenticationModel authenticationModel)
        {
            var hashedPassword = authenticationModel.Password.GetMD5Hash();
            authenticationModel.Password = hashedPassword;

            var foundUser = await _userService.GetUserByLoginAndPassword(authenticationModel);
            UserWithRoles userWithRoles = null;
            var isUserNotNull = foundUser != null;

            if(isUserNotNull)
            {
                var roles = await _userService.GetUserRolesById(foundUser.Id);
                userWithRoles = new UserWithRoles
                {
                    Id = foundUser.Id,
                    Roles = roles
                };
            }

            return new ValidationResult(isUserNotNull, userWithRoles);
        }

        public async Task<bool> RegisterUser(User userToRegister)
        {
            if(!IsPasswordValid(userToRegister.Password) || 
               !IsMailFormatValid(userToRegister.Email))
            {
                //return false;
                //return true;
            }

            return await _userService.RegisterUser(userToRegister);

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
