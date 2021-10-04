using System.Threading.Tasks;
using WebSocketChatCoreLib.AdditionalClasses;
using WebSocketChatCoreLib.Models;

namespace WebSocketChatCoreLib.Commands.MessageCommands
{
    public class InvalidCommand : Command
    {
        private InvalidCommand(string[] args) : base(args)
        {
        }

        public static InvalidCommand Create()
        {
            return new InvalidCommand(null);
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
