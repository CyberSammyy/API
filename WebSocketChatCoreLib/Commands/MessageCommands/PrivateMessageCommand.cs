using System;
using System.Threading.Tasks;
using WebSocketChatCoreLib.AdditionalClasses;
using WebSocketChatCoreLib.Models;

namespace WebSocketChatCoreLib.Commands.MessageCommands
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
            if (!sender.IsRegistered)
            {
                await socketHandler.SendMessageToYourself(new Message
                {
                    MessageText = "You are not registered user! Some features are disabled. \r\n Please, complete your registration by command /register Nickname Email Password Password. \r\n If have account, you can try to login by typing /login Nickname Password"
                }, sender.Id);

                return;
            }

            var clientId = Args[0];
            var targetClient = socketHandler.ConnectionManager[clientId];

            if(!targetClient.IsRegistered)
            {
                await socketHandler.SendMessageToYourself(new Message
                {
                    MessageText = Consts.Errors.TargetUserIsNotRegisteredErrorMessage,
                    Settings = new MessageSettings(MessageSettings.MessageSettingsPreset.CustomSettings, ConsoleColor.Red)
                }, sender.Id);

                return;
            }

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
            else
            {
                await socketHandler.SendMessageToYourself(new Message
                {
                    MessageText = Consts.Errors.UserDoesntExistErrorMessage,
                    Settings = new MessageSettings
                    {
                        Preset = MessageSettings.MessageSettingsPreset.CustomSettings,
                        MessageColor = ConsoleColor.Red
                    }
                }, sender.Id);
            }
        }
    }
}