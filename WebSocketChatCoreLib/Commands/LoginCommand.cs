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

        public LoginCommand(IUserRepository userRepository) : base(userRepository)
        {

        }
        private LoginCommand(string[] args) : base(args)
        {
        }

        public static LoginCommand Create(string[] args)
        {
            if (args.Length == ArgsCount)
            {
                return new LoginCommand(args);
            }

            throw new ArgumentException(
                string.Format(Consts.Messages.InvalidCommandArgumentsMessage, nameof(LoginCommand), ArgsCount));
        }

        public override async Task ProcessMessage(SocketUser sender, SocketHandler socketHandler)
        {
            var result = await new UserRepository().Login(new AuthenticationModel
            {
                Login = Args[0],
                Password = Args[1]
            });

            if (result.IsSuccessStatusCode)
            {
                sender.IsRegistered = true;
                await socketHandler.SendMessageToYourself(
                    new Message
                    {
                        SenderNickname = sender.Nickname,
                        MessageText = sender.Nickname + Consts.RegistrationCompletenessMessage + "TOKEN [" + await result.Content.ReadAsStringAsync() + "] END OF TOKEN",
                        Settings = new MessageSettings()
                    }, sender.Id);
            }
        }
    }
}
