using HelperClasses;
using System;
using System.Threading.Tasks;
using WebSocketChatCoreLib;

namespace WebSocketChatServerApp.Commands
{
    public class NicknameChangeCommand : Command
    {
        private const int ArgsCount = 1;

        private readonly IUserRepository _userRepository;

        private NicknameChangeCommand(string[] args, IUserRepository userRepository) : base(args)
        {
            _userRepository = userRepository;
        }

        public static NicknameChangeCommand Create(string[] args, IUserRepository userRepository)
        {
            if(args.Length == ArgsCount)
            {
                return new NicknameChangeCommand(args, userRepository);
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

            var result = await _userRepository.ChangeNickname(sender.Id, Args[0]);

            if(!result.IsSuccessStatusCode)
            {
                await socketHandler.SendMessageToYourself(new Message
                {
                    MessageText = result.ReasonPhrase + HelperConstants.UNIVERSAL_RESPONSE_STRING_SEPARATOR + result.StatusCode
                }, sender.Id);

                return;
            }

            await socketHandler.SendMessageToYourself(
                new Message
                {
                    MessageText = string.Format(Consts.Messages.NicknameChangedMessageToYourself, sender.Nickname),
                    Settings = sender.UserMessageSettings
                }, sender.Id);

            await socketHandler.SendPublicMessage(
                new Message
                {
                    MessageText = string.Format(Consts.Messages.NicknameChangedMessage, oldName, sender.Nickname),
                    Settings = sender.UserMessageSettings
                }, sender.Id);
        }
    }
}
