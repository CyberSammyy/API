using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebSocketChatServerApp;
using WebSocketChatServerApp.Commands;

namespace WebSocketChatCoreLib.Commands
{
    public class GetUserIdCommand : Command
    {
        private const int ArgsCount = 1;

        private GetUserIdCommand(string[] args) : base(args)
        {
        }

        public static GetUserIdCommand Create(string[] args)
        {
            if (args.Length == ArgsCount)
            {
                return new GetUserIdCommand(args);
            }

            throw new ArgumentException(
                string.Format(Consts.Messages.InvalidCommandArgumentsMessage, nameof(ColorChangeCommand), ArgsCount));
        }

        public override async Task ProcessMessage(SocketUser sender, SocketHandler socketHandler)
        {
            if (!sender.IsRegistered && !sender.IsConfirmed)
            {
                await socketHandler.SendMessageToYourself(new Message
                {
                    MessageText = Consts.Errors.NotRegisteredUserErrorMessage + Consts.Errors.NotConfirmedUserErrorMessage
                }, sender.Id);

                return;
            }

            var id = socketHandler.ConnectionManager[Args[0], true];

            await socketHandler.SendMessageToYourself(new Message
            {
                MessageText = $"ID of user \"{Args[0]}\": [{id}]"
            }, sender.Id);
        }
    }
}
