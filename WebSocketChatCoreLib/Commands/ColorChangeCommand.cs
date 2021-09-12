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
            if (!sender.IsRegistered)
            {
                await socketHandler.SendMessageToYourself(new Message
                {
                    MessageText = "You are not registered user! Some features are disabled. \r\n Please, complete your registration by command /register Nickname Email Password Password. \r\n If have account, you can try to login by typing /login Nickname Password"
                }, sender.Id);

                return;
            }

            if (Enum.TryParse<ConsoleColor>(Args[0], out var newColor))
            {
                sender.UserMessageSettings.MessageColor = newColor;
                sender.UserMessageSettings.Preset = MessageSettings.MessageSettingsPreset.CustomSettings;
                sender.UserMessageSettings.ReceivedMessageColor = newColor;

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
