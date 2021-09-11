using System;
using System.Threading.Tasks;

namespace WebSocketChatServerApp.Commands
{
    public class PrivateMessageCommand : Command
    {
        private const int MinArgsCount = 2;

        private PrivateMessageCommand(string[] args) : base(args)
        {
        }

        public static PrivateMessageCommand Create(string[] args)
        {
            if(args.Length >= MinArgsCount)
            {
                return new PrivateMessageCommand(args);
            }

            throw new ArgumentException(
                string.Format(Consts.Messages.InvalidCommandArgumentsMessage, nameof(PrivateMessageCommand), MinArgsCount));
        }

        public override async Task ProcessMessage(SocketUser sender, SocketHandler socketHandler)
        {
            var clientId = Args[0];
            var targetClient = socketHandler.ConnectionManager[clientId];

            if (targetClient != null)
            {
                var message = string.Format(
                    Consts.PrivateMessageFormat,
                    sender,
                    string.Join(' ', Args[1..]));

                await socketHandler.SendMessage(targetClient.WebSocket, new Message
                {
                    MessageText = message,
                    Settings = new MessageSettings
                    (
                        MessageSettings.MessageSettingsPreset.CustomSettings,
                        sender.UserMessageSettings.MessageColor,
                        targetClient.UserMessageSettings.MessageColor
                    )
                });
            }

        }
    }
}