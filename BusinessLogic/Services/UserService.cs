using AutoMapper;
using BusinessLogic.Classes;
using BusinessLogic.Interfaces;
using BusinessLogic.Models;
using DataAccess.Interfaces;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
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
                return await _userRepository.DeleteUserById(id);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<string>> GetUserRolesById(Guid id)
        {
            return await _userRepository.GetUserRolesById(id);
        }

        public async Task<User> GetUserByLoginAndPassword(AuthenticationModel userAuthData)
        {
            return _mapper.Map<User>(await _userRepository.GetUserByAuthData(userAuthData));
        }
    }
}
