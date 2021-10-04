using System;
using System.Net.Http;
using System.Threading.Tasks;
using WebSocketChatCoreLib.Models;
using WebSocketChatServer;

namespace WebSocketChatCoreLib.Interfaces
{
    public interface IUserRepository
    {
        public Task<HttpResponseMessage> RegisterUser(SocketUser userToRegister);
        public Task<(HttpResponseMessage response, Guid idFromDataBase)> Login(AuthenticationModel loginData);
        public Task<HttpResponseMessage> ChangeNickname(Guid userId, string newNickname);
        public Task<HttpResponseMessage> ChangePassword(Guid userId, string newPassword);
    }
}
