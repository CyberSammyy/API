using DistributedModels;
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
        Task<(HttpResponseMessage response, Guid idFromDataBase)> Login(AuthenticationModel authenticationModel);
        Task<HttpResponseMessage> Register(User userToRegister);
        Task<HttpResponseMessage> ChangeNickname(StringUserFieldChangeModel changedNickname);
        Task<HttpResponseMessage> ChangePassword(Guid userId, string newPasswordHash);
    }
}