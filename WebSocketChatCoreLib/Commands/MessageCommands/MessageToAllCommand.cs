using System;
using System.Threading.Tasks;
using WebSocketChatCoreLib.AdditionalClasses;
using WebSocketChatCoreLib.Models;

namespace WebSocketChatCoreLib.Commands.MessageCommands
{
    public class MessageToAllCommand : Command
    {
        private const int MinArgsCount = 1;

        private MessageToAllCommand(string[] args) : base(args)
        {
        }

        public static MessageToAllCommand Create(string[] args)
        {
            if (args.Length >= MinArgsCount)
            {
                return new MessageToAllCommand(args);
            }

            throw new ArgumentException(
                string.Format(Consts.Messages.InvalidCommandArgumentsMessage, nameof(MessageToAllCommand), MinArgsCount));
        }

        public override async Task ProcessMessage(SocketUser sender, SocketHandler socketHandler)
        {
            await socketHandler.SendPublicMessage(new Message
            {
                MessageText = string.Join(' ', Args[0..]),
                Settings = sender.UserMessageSettings,
                SenderNickname = sender.Nickname
            },
            sender.Id);
        }
    }
}
