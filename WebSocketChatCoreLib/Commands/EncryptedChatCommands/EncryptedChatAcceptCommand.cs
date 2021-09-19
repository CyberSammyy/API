using System;
using System.Threading.Tasks;
using WebSocketChatServerApp;
using WebSocketChatServerApp.Commands;

namespace WebSocketChatCoreLib.Commands.EncryptedChatCommands
{
    public class EncryptedChatAcceptCommand : Command
    {
        private const int MinArgsCount = 1;

        private EncryptedChatAcceptCommand(string[] args) : base(args)
        {
        }

        public static EncryptedChatAcceptCommand Create(string[] args)
        {
            if (args.Length >= MinArgsCount)
            {
                return new EncryptedChatAcceptCommand(args);
            }

            throw new ArgumentException(
                string.Format(Consts.Messages.InvalidCommandArgumentsMessage, nameof(EncryptedChatAcceptCommand), MinArgsCount));
        }

        public override async Task ProcessMessage(SocketUser sender, SocketHandler socketHandler)
        {

        }
    }
}
