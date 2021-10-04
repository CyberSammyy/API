using System;
using System.Threading.Tasks;
using WebSocketChatCoreLib.AdditionalClasses;
using WebSocketChatCoreLib.Models;

namespace WebSocketChatCoreLib.Commands.WebSocketChatCoreLib.Commands.RegistrationAndAuthenticationCommands
{
    public class LogoutCommand : Command
    {
        private const int MinArgsCount = 0;

        private LogoutCommand(string[] args) : base(args)
        {
        }

        public static LogoutCommand Create(string[] args)
        {
            if (args.Length >= MinArgsCount)
            {
                return new LogoutCommand(args);
            }

            throw new ArgumentException(
                string.Format(Consts.Messages.InvalidCommandArgumentsMessage, nameof(LogoutCommand), MinArgsCount));
        }

        public override async Task ProcessMessage(SocketUser sender, SocketHandler socketHandler)
        {
            if(!sender.IsLoggedIn)
            {
                await socketHandler.SendMessageToYourself(new Message
                {
                    MessageText = Consts.Errors.NotLoggedInUserErrorMessage
                }, sender.Id);

                return;
            }

            var oldName = sender.Nickname;
            var oldSettings = sender.UserMessageSettings;

            await sender.Clear(socketHandler);

            await socketHandler.SendMessageToYourself(new Message
            {
                MessageText = string.Format(Consts.UserLoggedOutMessageToYourself, sender.Nickname)
            }, sender.Id);

            await socketHandler.SendPublicMessage(new Message
            {
                MessageText = string.Format(Consts.UserLoggedOutMessage, oldName),
                Settings = oldSettings,
                SenderNickname = oldName
            },
            sender.Id);
        }
    }
}
