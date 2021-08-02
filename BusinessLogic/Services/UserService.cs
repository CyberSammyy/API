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
                catch (NullReferenceException ex)
                {
                    return Guid.Empty;
                }
                catch (Exception ex)
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
            List<User> mappedCollectionUsers = new List<User>();
            if (users == null)
            {
                return new List<User>();
            }
            {
                foreach (var user in users)
                {
                    mappedCollectionUsers.Add(_mapper.Map<User>(user));
                }
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

        public IEnumerable<string> GetUserRolesById(Guid id)
        {
            throw new NotImplementedException();
        }

        public User GetUserByLoginAndPassword(AuthenticationModel userAuthData)
        {
            throw new NotImplementedException();
        }
    }
}
