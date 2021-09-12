using AutoMapper;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebSocketChatServer;
using WebSocketChatServerApp;

namespace WebSocketChatCoreLib
{
    public class UserRepository : IUserRepository
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserRepository()
        {

        }
        public UserRepository(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }
        public async Task<HttpResponseMessage> Login(AuthenticationModel loginData)
        {
            try
            {
                var result = await new UserService().Login(loginData);
                
                return result;
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // Variable is declared but never used
            {
                return new HttpResponseMessage
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };
            }
        }

        public async Task<HttpResponseMessage> RegisterUser(SocketUser userToRegister)
        {
            try
            {
                var result = await new UserService().Register(new User
                {
                    Email = userToRegister.Email,
                    Password = userToRegister.Password,
                    PhoneNumber = userToRegister.PhoneNumber,
                    Id = userToRegister.Id,
                    Nickname = userToRegister.Nickname
                });

                return result;
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // Variable is declared but never used
            {
                return new HttpResponseMessage
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };
            }
        }
    }
}
