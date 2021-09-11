using System;
using System.Threading.Tasks;
using WebSocketChatServerApp.Commands;

namespace WebSocketChatServerApp
{
    public class ColorChangeCommand : Command
    {
        private const int ArgsCount = 1;

        private ColorChangeCommand(string[] args) : base(args)
        {
        }

        public static ColorChangeCommand Create(string[] args)
        {
            if (args.Length == ArgsCount)
            {
                return new ColorChangeCommand(args);
            }

            throw new ArgumentException(
                string.Format(Consts.Messages.InvalidCommandArgumentsMessage, nameof(ColorChangeCommand), ArgsCount));
        }

        public override async Task ProcessMessage(SocketUser sender, SocketHandler socketHandler)
        {
            if (Enum.TryParse<ConsoleColor>(Args[0], out var newColor))
            {
                sender.UserMessageSettings.MessageColor = newColor;
                sender.UserMessageSettings.Preset = MessageSettings.MessageSettingsPreset.CustomSettings;


                await socketHandler.SendMessage(sender.WebSocket, new Message
                {
                    MessageText = Consts.ColorChangeMessage,
                    SenderNickname = sender.Nickname,
                    Settings = sender.UserMessageSettings
                });
            }
        }
    }
}
