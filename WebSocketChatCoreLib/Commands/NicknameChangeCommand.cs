using System;
using System.Threading.Tasks;
using WebSocketChatServer;

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
                    MessageText = Consts.Errors.NotRegisteredUserErrorMessage
                }, sender.Id);

                return;
            }

            var oldName = sender.ToString();
            sender.Nickname = Args[0];

            var result = await new UserService().ChangeUserData(new User
            {
                Email = sender.Email,
                Nickname = Args[0],
                Id = sender.Id,
                Password = sender.Password,
                PhoneNumber = sender.PhoneNumber,
                Token = sender.Token
            });

            if(!result.IsSuccessStatusCode)
            {
                await socketHandler.SendMessageToYourself(new Message
                {
                    MessageText = result.ReasonPhrase + "|" + result.StatusCode
                }, sender.Id);

                return;
            }

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
