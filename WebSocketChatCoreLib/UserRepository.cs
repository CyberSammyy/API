using AutoMapper;
using MD5Generator;
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

        public UserRepository(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<HttpResponseMessage> ChangeNickname(Guid userId, string newNickname)
        {
            try
            {
                var result = await _userService.ChangeNickname(userId, newNickname);
                return result;
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch(Exception ex)
#pragma warning restore CS0168 // Variable is declared but never used
            {
                return new HttpResponseMessage
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };
            }
        }

        public async Task<HttpResponseMessage> ChangePassword(Guid userId, string newPassword)
        {
            try
            {
                var result = await _userService.ChangePassword(userId, newPassword.GetMD5Hash());
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

        public async Task<(HttpResponseMessage response, Guid idFromDataBase)> Login(AuthenticationModel loginData)
        {
            try
            {
                var result = await _userService.Login(loginData);
                
                return result;
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // Variable is declared but never used
            {
                return (new HttpResponseMessage
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                }, Guid.Empty);
            }
        }

        public async Task<HttpResponseMessage> RegisterUser(SocketUser userToRegister)
        {
            try
            {
                var result = await _userService.Register(new User
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
