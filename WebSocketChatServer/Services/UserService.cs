using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System;
using HelperClasses;
using DistributedModels;

namespace WebSocketChatServer
{
    public class UserService : IUserService
    {
        public string Token { get; set; } = string.Empty;

        public async Task<HttpResponseMessage> Register(User userToRegister)
        {
            using (var client = new HttpClient())
            {
                var response = await client.PostAsJsonAsync(Constants.APP_PATH + @"/Users/register", userToRegister);

                userToRegister.Token = await response.Content.ReadAsStringAsync();
                Token = userToRegister.Token;

                return response;
            }
        }

        public async Task<(HttpResponseMessage response, Guid idFromDataBase)> Login(AuthenticationModel authenticationModel)
        {
            using (var client = new HttpClient())
            {
                var responce = await client.PostAsJsonAsync(Constants.APP_PATH + @"/Users/login", authenticationModel);

                var tokenAndIdFromDb = await responce.Content.ReadAsStringAsync();

                var parsedTokenAndId = TokenAndIdParser.ParseTokenIdString(tokenAndIdFromDb);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", parsedTokenAndId.token);

                if(responce.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Token = parsedTokenAndId.token;
                    responce.Headers.Add("Token", Token);
                }

                return (responce, parsedTokenAndId.id);
            }
        }

        public async Task<HttpResponseMessage> GetUserById(Guid id)
        {
            using (var client = new HttpClient())
            {
                var responce = await client.PostAsJsonAsync(Constants.APP_PATH + @"/Users/{id}", id);


                return responce;
            }
        }

        public async Task<HttpResponseMessage> ChangeNickname(StringUserFieldChangeModel changedNickname)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);

                var responce = await client.PutAsJsonAsync(Constants.APP_PATH + @"/Users/changeNickname", changedNickname);

                return responce;
            }
        }

        public async Task<HttpResponseMessage> ChangePassword(Guid userId, string newPasswordHash)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);

                var responce = await client.PutAsJsonAsync(Constants.APP_PATH + @"/Users/changePassword", new
                {
                    userId = userId,
                    newPasswordHash = newPasswordHash
                });

                return responce;
            }
        }

        public async Task<Dictionary<string, string>> GetTokenDictionary(string userName, string password)
        {
            var pairs = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("grant_type", "password" ),
                    new KeyValuePair<string, string>("Nickname", userName ),
                    new KeyValuePair<string, string>("Password", password )
                };
            var content = new FormUrlEncodedContent(pairs);

            using (var client = new HttpClient())
            {
                var response = await client.PostAsync(Constants.APP_PATH + "/Token", content);
                var result = await response.Content.ReadAsStringAsync();
                Dictionary<string, string> tokenDictionary =
                    JsonConvert.DeserializeObject<Dictionary<string, string>>(result);

                return tokenDictionary;
            }
        }

        public HttpClient CreateClient(string accessToken = null)
        {
            var client = new HttpClient();
            if (!string.IsNullOrWhiteSpace(accessToken))
            {
                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
            }

            return client;
        }

        //// получаем информацию о клиенте 
        //public async Task<string> GetUserInfo(string token)
        //{
        //    using (var client = CreateClient(token))
        //    {
        //        var response = await client.GetAsync(Constants.APP_PATH + "/api/Account/UserInfo");
        //        return await response.Content.ReadAsStringAsync();
        //    }
        //}
        //
        //// обращаемся по маршруту api/values 
        //public async Task<string> GetValues(string token)
        //{
        //    using (var client = CreateClient(token))
        //    {
        //        var response = await client.GetAsync(Constants.APP_PATH + "/api/values");
        //        return await response.Content.ReadAsStringAsync();
        //    }
        //}
    }
}
