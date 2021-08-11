using AutoMapper;
using BusinessLogic.Classes;
using BusinessLogic.HelperClasses;
using BusinessLogic.Interfaces;
using BusinessLogic.Models;
using BusinessLogic.Services;
using DataAccess.Interfaces;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        private readonly IRolesRepository _rolesRepository;

        private readonly IMapper _mapper;

        private readonly IMailService _mailService;

        private readonly IMailExchangerService _mailExchangerService;

        public UserService(IUserRepository userRepository, 
            IRolesRepository rolesRepository, 
            IMapper mapper, 
            IMailService mailService,
            IMailExchangerService mailExchangerService)
        {
            _userRepository = userRepository;
            _rolesRepository = rolesRepository;
            _mapper = mapper;
            _mailService = mailService;
            _mailExchangerService = mailExchangerService;
        }

        public async Task<Guid> AddUser(User user)
        {
            if (UserValidation.IsUserValid(user))
            {
                try
                {
                    var dto = _mapper.Map<UserDTO>(user);
                    await _userRepository.AddUser(dto);
                }
#pragma warning disable CS0168 // Variable is declared but never used
                catch (NullReferenceException ex)
#pragma warning restore CS0168 // Variable is declared but never used
                {
                    return Guid.Empty;
                }
#pragma warning disable CS0168 // Variable is declared but never used
                catch (Exception ex)
#pragma warning restore CS0168 // Variable is declared but never used
                {
                    return Guid.Empty;
                }
            }

            return user.Id;
        }

        public async Task<User> GetUserById(Guid id)
        {
            var user = await _userRepository.GetUserById(id);

            if (user == null)
            {
                throw new NullReferenceException("User is empty!");
            }

            var newUser = _mapper.Map<User>(user);

            return newUser;
        }

        public IEnumerable<User> GetUsers()
        {
            var users = _userRepository.GetUsers();

            IEnumerable<User> mappedCollectionUsers = new List<User>();

            if (users == null)
            {
                return new List<User>();
            }
            else
            {
                mappedCollectionUsers = _mapper.Map<IEnumerable<User>>(users);

                return mappedCollectionUsers;
            }
        }

        public async Task<bool> PutUser(User user)
        {
            try
            {
                var mappedUser = _mapper.Map<UserDTO>(user);

                return await _userRepository.PutUser(mappedUser);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteUserById(Guid id)
        {
            try
            {
                var roles = await _rolesRepository.GetRolesById(id);
                foreach(var role in roles)
                {
                    if(!(await _rolesRepository.RemoveRole(id, role.RoleName)))
                    {
                        break;
                    }
                }

                return await _userRepository.DeleteUserById(id);
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // Variable is declared but never used
            {
                return false;
            }
        }

        public async Task<User> GetUserByLoginAndPassword(AuthenticationModel userAuthData)
        {
            return _mapper.Map<User>(await _userRepository.GetUserByAuthData(userAuthData));
        }

        public async Task<bool> RegisterUser(User userToRegister)
        {
            userToRegister.Id = Guid.NewGuid();
            try
            {
                return await _userRepository.RegisterUser(_mapper.Map<UserDTO>(userToRegister));
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch(Exception ex)
#pragma warning restore CS0168 // Variable is declared but never used
            {
                return false;
            }
        }
        public void AddUserMail(Guid userId, string mail, string path)
        {
            var confirmationModel = new ConfirmationMessageModel
            {
                ConfirmationMessage = StringGenerator.GenerateString(),
                UserId = userId
            };
            var modelToSerialize = JsonSerializer.Serialize(confirmationModel);
            var messageToSend = EncryptionHelper.Encrypt(modelToSerialize);

            _mailService.SaveMailAddress(new EmailDTO
            {
                ConfirmationMessage = confirmationModel.ConfirmationMessage,
                Email = mail,
                IsConfirmed = false,
                UserId = userId
            });
            var confirmationString = $"{path}/users/confirm?message={messageToSend}";

            _mailExchangerService.SendMessage(
                mail,
                "Email confirmation",
                confirmationString);

            WriteStringToFile(confirmationString);
        }

        public async Task<ConfirmationResult> ConfirmEmail(string message)
        {
            try
            {
                var decrypted = EncryptionHelper.Decrypt(message);
                var model = JsonSerializer.Deserialize<ConfirmationMessageModel>(decrypted);
                var isSuccessful = _mailService.ConfirmMail(model);
                return new ConfirmationResult
                {
                    IsSuccessful = await isSuccessful,
                    UserId = model.UserId
                };
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // Variable is declared but never used
            {
                return new ConfirmationResult { IsSuccessful = false };
            }

        }
        private void WriteStringToFile(string message)
        {
            using (var streamWriter = new StreamWriter("confirmationString.txt"))
            {
                streamWriter.WriteLine(message);
            }
        }
    }
}
