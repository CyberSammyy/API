using System;
using System.Threading.Tasks;

namespace WebSocketChatServerApp.Commands
{
    public class NicknameChangeCommand : Command
    {
        private const int ArgsCount = 1;

        private NicknameChangeCommand(string[] args) : base(args)
        {
        }

        public static NicknameChangeCommand Create(string[] args)
        {
            if(args.Length == ArgsCount)
            {
                return new NicknameChangeCommand(args);
            }

            throw new ArgumentException(
                string.Format(Consts.Messages.InvalidCommandArgumentsMessage, nameof(NicknameChangeCommand), ArgsCount));
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

            var oldName = sender.ToString();
            sender.Nickname = Args[0];
            var message = string.Format(Consts.Messages.NicknameChangedMessage, oldName, sender.Nickname);

            await socketHandler.SendPublicMessage(
                new Message
                {
                    MessageText = message,
                    Settings = sender.UserMessageSettings
                }, sender.Id);
        }
    }
}
