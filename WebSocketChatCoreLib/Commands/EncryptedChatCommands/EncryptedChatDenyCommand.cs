﻿using System;
using System.Threading.Tasks;
using WebSocketChatServerApp;
using WebSocketChatServerApp.Commands;

namespace WebSocketChatCoreLib.Commands.EncryptedChatCommands
{
    public class EncryptedChatDenyCommand : Command
    {
        private const int MinArgsCount = 1;

        private EncryptedChatDenyCommand(string[] args) : base(args)
        {
        }

        public static EncryptedChatDenyCommand Create(string[] args)
        {
            if (args.Length >= MinArgsCount)
            {
                return new EncryptedChatDenyCommand(args);
            }

            throw new ArgumentException(
                string.Format(Consts.Messages.InvalidCommandArgumentsMessage, nameof(EncryptedChatDenyCommand), MinArgsCount));
        }

        public override async Task ProcessMessage(SocketUser sender, SocketHandler socketHandler)
        {
            if(!sender.EncryptedSessionSettings.IsEncryptedChatRequestTaken)
            {
                await socketHandler.SendMessageToYourself(new Message
                {
                    MessageText = Consts.Errors.UserDoesntHaveActiveIncomingRequestsErrorMessage,
                    Settings = new MessageSettings
                    {
                        Preset = MessageSettings.MessageSettingsPreset.CustomSettings,
                        MessageColor = ConsoleColor.Red
                    }
                }, sender.Id);

                return;
            }

            SocketUser requester;
            try
            {
                requester = socketHandler.ConnectionManager[sender.EncryptedSessionSettings.IncomingRequestingId];
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // Variable is declared but never used
            {
                return;
            }

            if(requester == null)
            {
                throw new ArgumentNullException(string.Format(Consts.ExceptionMessages.ArgumentNullExceptionMessage, nameof(requester)));
            }

            sender.EncryptedSessionSettings.IncomingRequestsCounter--;
            requester.EncryptedSessionSettings.OutcomingRequestsCounter--;

            requester.EncryptedSessionSettings.IsEncryptedChatRequestSent = false;
            requester.EncryptedSessionSettings.OutcomingRequestingId = Guid.Empty;

            sender.EncryptedSessionSettings.IncomingRequestingId = Guid.Empty;
            sender.EncryptedSessionSettings.IsEncryptedChatRequestTaken = false;

            await socketHandler.SendMessageToYourself(new Message
            {
                MessageText = string.Format(Consts.EncryptionChatRequestDeniedMessage, sender.Nickname)
            }, sender.Id);

            await socketHandler.SendPublicMessage(new Message
            {
                MessageText = string.Format(Consts.EncryptionChatRequestDeniedMessage, sender.Nickname),
                Settings = new MessageSettings
                {
                    Preset = MessageSettings.MessageSettingsPreset.CustomSettings,
                    MessageColor = ConsoleColor.Red
                }
            }, sender.Id);
        }
    }
}
