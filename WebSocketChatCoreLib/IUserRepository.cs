using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebSocketChatServer;
using WebSocketChatServerApp;

namespace WebSocketChatCoreLib
{
    public interface IUserRepository
    {
        public Task<HttpResponseMessage> RegisterUser(SocketUser userToRegister);
        public Task<(HttpResponseMessage response, Guid idFromDataBase)> Login(AuthenticationModel loginData);
        public Task<HttpResponseMessage> ChangeNickname(Guid userId, string newNickname);
        public Task<HttpResponseMessage> ChangePassword(Guid userId, string newPassword);
    }
}
