using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebSocketChatServer
{
    public class UserInterface
    {
        private readonly IUserService _userService;

        public UserInterface(UserService userService)
        {
            _userService = userService;
        }

        public async Task Run(string[] args)
        {
            Console.WriteLine("Input login");
            string userName = Console.ReadLine();

            Console.WriteLine("Input email (You can input random text if you want to login)");
            string email = Console.ReadLine();

            Console.WriteLine("Input phone number (You can input random text if you want to login)");
            string phoneNumber = Console.ReadLine();

            Console.WriteLine("Input password");
            string password = Console.ReadLine();

            HttpResponseMessage requestResult = new HttpResponseMessage();

            Console.WriteLine("Choose, what you want to do: Login or Registration");
            var choose = Console.ReadLine();
            if(choose == "Login")
            {
                try
                {
                    requestResult = (await _userService.Login(new AuthenticationModel
                    {
                        Login = userName,
                        Password = password
                    })).response;
                }
                catch(Exception ex)
                {
                    Console.WriteLine(Helpers.FailMessage(ex));
                }
            }
            else if(choose == "Registration")
            {
                try
                {
                    requestResult = await _userService.Register(new User
                    {
                        Nickname = userName,
                        Password = password,
                        PhoneNumber = int.Parse(phoneNumber),
                        Email = email
                    });
                    Console.WriteLine("Registration status: {0}", requestResult.StatusCode);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(Helpers.FailMessage(ex));
                }
            }

            Dictionary<string, string> tokenDictionary = await _userService.GetTokenDictionary(userName, password);
            _userService.Token = await requestResult.Content.ReadAsStringAsync();
            Console.WriteLine();
            Console.WriteLine("Access Token:");
            Console.WriteLine(_userService.Token);

            //Console.WriteLine();
            //string userInfo = await _userService.GetUserInfo(_userService.Token);
            //Console.WriteLine("Пользователь:");
            //Console.WriteLine(userInfo);
            //
            //Console.WriteLine();
            //string values = await _userService.GetValues(_userService.Token);
            //Console.WriteLine("Values:");
            //Console.WriteLine(values);

            Console.Read();
        }
    }
}
