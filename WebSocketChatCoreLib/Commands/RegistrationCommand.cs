using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebSocketChatServer;
using WebSocketChatServerApp;
using WebSocketChatServerApp.Commands;

namespace WebSocketChatCoreLib.Commands
{
    class RegistrationCommand : Command
    {
        private const int ArgsCount = 4;

        public RegistrationCommand(IUserRepository userRepository) : base(userRepository)
        {

        }
        private RegistrationCommand(string[] args) : base(args)
        {
        }

        public static RegistrationCommand Create(string[] args)
        {
            if (args.Length == ArgsCount)
            {
                return new RegistrationCommand(args);
            }

            throw new ArgumentException(
                string.Format(Consts.Messages.InvalidCommandArgumentsMessage, nameof(RegistrationCommand), ArgsCount));
        }

        public override async Task ProcessMessage(SocketUser sender, SocketHandler socketHandler)
        {
            if(Args[2] != Args[3])
            {
                throw new Exception("Passwords doesn't match!");
            }
            //var result = await new UserService().Register(
            //    new User
            //    {
            //        Nickname = Args[0],
            //        Email = Args[1],
            //        Password = Args[2],
            //        Id = Guid.NewGuid()
            //    });
            var result = await new UserRepository().RegisterUser(sender);

            if(result.IsSuccessStatusCode)
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
