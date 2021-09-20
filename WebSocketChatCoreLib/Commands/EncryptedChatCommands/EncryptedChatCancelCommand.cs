using System;
using System.Threading.Tasks;
using WebSocketChatServerApp;
using WebSocketChatServerApp.Commands;

namespace WebSocketChatCoreLib.Commands.EncryptedChatCommands
{
    public class EncryptedChatCancelCommand : Command
    {
        private const int MinArgsCount = 0;

        private EncryptedChatCancelCommand(string[] args) : base(args)
        {
        }

        public static EncryptedChatCancelCommand Create(string[] args)
        {
            if (args.Length >= MinArgsCount)
            {
                return new EncryptedChatCancelCommand(args);
            }

            throw new ArgumentException(
                string.Format(Consts.Messages.InvalidCommandArgumentsMessage, nameof(EncryptedChatCancelCommand), MinArgsCount));
        }

        public override async Task ProcessMessage(SocketUser sender, SocketHandler socketHandler)
        {
            if (!sender.EncryptedSessionSettings.IsEncryptedChatRequestSent)
            {
                await socketHandler.SendMessageToYourself(new Message
                {
                    MessageText = Consts.Errors.UserDoesntHaveActiveOutcomingRequestsErrorMessage,
                    Settings = new MessageSettings
                    {
                        Preset = MessageSettings.MessageSettingsPreset.CustomSettings,
                        MessageColor = ConsoleColor.Red
                    }
                }, sender.Id);

                return;
            }

            var foundUser = socketHandler.ConnectionManager[sender.EncryptedSessionSettings.OutcomingRequestingId];

            if (foundUser == null)
            {
                throw new ArgumentNullException(string.Format(Consts.ExceptionMessages.ArgumentNullExceptionMessage, nameof(foundUser)));
            }

            sender.EncryptedSessionSettings.OutcomingRequestsCounter--;
            foundUser.EncryptedSessionSettings.IncomingRequestsCounter--;

            foundUser.EncryptedSessionSettings.IsEncryptedChatRequestTaken = false;
            foundUser.EncryptedSessionSettings.IncomingRequestingId = Guid.Empty;

            sender.EncryptedSessionSettings.OutcomingRequestingId = Guid.Empty;
            sender.EncryptedSessionSettings.IsEncryptedChatRequestSent = false;

            await socketHandler.SendMessageToYourself(new Message
            {
                MessageText = string.Format(Consts.EncryptionChatRequestCanceledMessage, sender.Nickname)
            }, sender.Id);

            await socketHandler.SendPublicMessage(new Message
            {
                MessageText = string.Format(Consts.EncryptionChatRequestCanceledMessage, sender.Nickname),
                Settings = new MessageSettings
                {
                    Preset = MessageSettings.MessageSettingsPreset.CustomSettings,
                    MessageColor = ConsoleColor.Red
                }
            }, sender.Id);
        }
    }
}
