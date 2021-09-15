using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebSocketChatServer
{
    public interface IUserService
    {
        string Token { get; set; }
        HttpClient CreateClient(string accessToken = null);
        Task<Dictionary<string, string>> GetTokenDictionary(string userName, string password);
        //Task<string> GetUserInfo(string token);
        //Task<string> GetValues(string token);
        Task<(HttpResponseMessage response, Guid idFromDataBase)> Login(AuthenticationModel authenticationModel);
        Task<HttpResponseMessage> Register(User userToRegister);
        Task<HttpResponseMessage> ChangeNickname(Guid userId, string newNickname);
        Task<HttpResponseMessage> ChangePassword(Guid userId, string newPasswordHash);
        //Task<HttpResponseMessage> ChangeUserData(User updatedUser);
    }
}