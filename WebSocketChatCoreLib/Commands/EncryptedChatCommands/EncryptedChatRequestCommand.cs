using System;
using System.Threading.Tasks;
using WebSocketChatServerApp;
using WebSocketChatServerApp.Commands;

namespace WebSocketChatCoreLib.Commands
{
    public class EncryptedChatRequestCommand : Command
    {
        private const int MinArgsCount = 1;

        private EncryptedChatRequestCommand(string[] args) : base(args)
        {
        }

        public static EncryptedChatRequestCommand Create(string[] args)
        {
            if (args.Length >= MinArgsCount)
            {
                return new EncryptedChatRequestCommand(args);
            }

            throw new ArgumentException(
                string.Format(Consts.Messages.InvalidCommandArgumentsMessage, nameof(EncryptedChatRequestCommand), MinArgsCount));
        }

        public override async Task ProcessMessage(SocketUser sender, SocketHandler socketHandler)
        {
            var foundUser = socketHandler.ConnectionManager[Args[0]];

            if(!foundUser.IsRegistered || !foundUser.IsLoggedIn /*|| !foundUser.IsConfirmed*/) //TODO REMOVE THIS COMMENT AND CONFIRM ALL EXISTING USERS
            {
                await socketHandler.SendMessageToYourself(new Message
                {
                    MessageText = Consts.Errors.CanNotStartEncryptedSessionWithUnauthorizedUserErrorMessage,
                    Settings = new MessageSettings
                    {
                        Preset = MessageSettings.MessageSettingsPreset.CustomSettings,
                        MessageColor = ConsoleColor.Red
                    }
                }, sender.Id);

                return;
            }

            if(foundUser.EncryptedSessionSettings.IsEncryptedSessionStarted || 
                sender.EncryptedSessionSettings.IsEncryptedSessionStarted)
            {
                await socketHandler.SendMessageToYourself(new Message
                {
                    MessageText = Consts.Errors.CanNotStartEncryptedSessionErrorMessage,
                    Settings = new MessageSettings
                    {
                        Preset = MessageSettings.MessageSettingsPreset.CustomSettings,
                        MessageColor = ConsoleColor.Red
                    }
                }, sender.Id);

                return;
            }

            sender.EncryptedSessionSettings.OutcomingRequestsCounter++;
            foundUser.EncryptedSessionSettings.IncomingRequestsCounter++;

            sender.EncryptedSessionSettings.OutcomingRequestingId = foundUser.Id;
            sender.EncryptedSessionSettings.IsEncryptedChatRequestSent = true;

            await socketHandler.SendMessageToYourself(new Message
            {
                MessageText = Consts.SuccessMessage + 
                    string.Format(Consts.OutcomingRequestsCountMessage, sender.EncryptedSessionSettings.OutcomingRequestsCounter),
                Settings = new MessageSettings
                {
                    Preset = MessageSettings.MessageSettingsPreset.CustomSettings,
                    MessageColor = ConsoleColor.Green
                }
            }, sender.Id);

            await socketHandler.SendPublicMessage(new Message
            {
                MessageText = string.Format(Consts.EncryptedSessionRequestedMessage, sender.Nickname, 
                    sender.EncryptedSessionSettings.IncomingRequestsCounter),
                SenderNickname = sender.Nickname,
                Settings = new MessageSettings
                {
                    Preset = MessageSettings.MessageSettingsPreset.CustomSettings,
                    MessageColor = sender.UserMessageSettings.MessageColor,
                    ReceivedMessageColor = foundUser.UserMessageSettings.ReceivedMessageColor
                }
            }, sender.Id);

            foundUser.EncryptedSessionSettings.IsEncryptedChatRequestTaken = true;
            foundUser.EncryptedSessionSettings.IncomingRequestingId = sender.Id;
        }
    }
}
