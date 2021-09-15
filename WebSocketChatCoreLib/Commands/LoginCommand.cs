using HelperClasses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebSocketChatServer;
using WebSocketChatServerApp;
using WebSocketChatServerApp.Commands;

namespace WebSocketChatCoreLib.Commands
{
    public class LoginCommand : Command
    {
        private const int ArgsCount = 2;
        private readonly IUserRepository _userRepository;

        private LoginCommand(string[] args, IUserRepository userRepository) : base(args)
        {
            _userRepository = userRepository;
        }

        public static LoginCommand Create(string[] args, IUserRepository userRepository)
        {
            if (args.Length == ArgsCount)
            {
                return new LoginCommand(args, userRepository);
            }

            throw new ArgumentException(
                string.Format(Consts.Messages.InvalidCommandArgumentsMessage, nameof(LoginCommand), ArgsCount));
        }

        public override async Task ProcessMessage(SocketUser sender, SocketHandler socketHandler)
        {
            if(sender.IsLoggedIn)
            {
                await socketHandler.SendMessageToYourself(new Message
                {
                    MessageText = string.Format(Consts.Errors.UserIsAlreadyLoggedInErrorMessage, sender.Nickname)
                }, sender.Id);
            }

            var result = await _userRepository.Login(new AuthenticationModel
            {
                Login = Args[0],
                Password = Args[1]
            });

            if (result.response.IsSuccessStatusCode)
            {
                var dataFromParsedString = await result.response.Content.ReadAsStringAsync();

                sender.IsRegistered = true;
                sender.Nickname = Args[0];
                sender.Token = TokenAndIdParser.RemoveIdFromTokenIdString(dataFromParsedString);
                sender.IsLoggedIn = true;
                sender.Id = result.idFromDataBase;
                sender.Email = TokenAndIdParser.ParseTokenIdString(dataFromParsedString).email;
                sender.PhoneNumber = Convert.ToInt32(TokenAndIdParser.ParseTokenIdString(dataFromParsedString).phoneNumber);

                await socketHandler.SendMessageToYourself(
                    new Message
                    {
                        SenderNickname = sender.Nickname,
                        MessageText = string.Format(Consts.LoginCompletenessMessage, sender.Nickname) + "\r\n" + "TOKEN [" + await result.response.Content.ReadAsStringAsync() + "] END OF TOKEN",
                        Settings = new MessageSettings()
                    }, sender.Id);

                await socketHandler.SendPublicMessage(new Message
                {
                    MessageText = string.Format(Consts.UserLoginMessage, sender.Nickname)
                }, sender.Id);
            }
        }
    }
}
