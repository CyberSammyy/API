using System;
using System.Threading.Tasks;
using WebSocketChatCoreLib;
using WebSocketChatServer;

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

            var result = await _userRepository.ChangeUserData(new User
            {
                Nickname = Args[0],
                Email = sender.Email,
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
