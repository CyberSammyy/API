using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebSocketChatServerApp;
using WebSocketChatServerApp.Commands;

namespace WebSocketChatCoreLib.Commands
{
    public class RemoveUserAdminCommand : Command
    {
        private const int ArgsCount = 1;

        private RemoveUserAdminCommand(string[] args) : base(args)
        {
        }

        public static RemoveUserAdminCommand Create(string[] args)
        {
            if (args.Length == ArgsCount)
            {
                return new RemoveUserAdminCommand(args);
            }

            throw new ArgumentException(
                string.Format(Consts.Messages.InvalidCommandArgumentsMessage, nameof(ColorChangeCommand), ArgsCount));
        }

        public override async Task ProcessMessage(SocketUser sender, SocketHandler socketHandler)
        {

            await socketHandler.SendMessage(sender.WebSocket,
                new Message
                {
                    MessageText = Consts.Messages.InvalidCommandMessage,
                    SenderNickname = sender.Nickname,
                    Settings = sender.UserMessageSettings
                });
        }
    }
}
