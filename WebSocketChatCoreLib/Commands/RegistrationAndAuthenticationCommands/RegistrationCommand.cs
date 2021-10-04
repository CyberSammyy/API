using System;
using System.Threading.Tasks;
using WebSocketChatCoreLib.AdditionalClasses;
using WebSocketChatCoreLib.Interfaces;
using WebSocketChatCoreLib.Models;

namespace WebSocketChatCoreLib.Commands.RegistrationAndAuthenticationCommands
{
    class RegistrationCommand : Command
    {
        private const int ArgsCount = 4;

        private readonly IUserRepository _userRepository;

        private RegistrationCommand(string[] args, IUserRepository userRepository) : base(args)
        {
            _userRepository = userRepository;
        }

        public static RegistrationCommand Create(string[] args, IUserRepository userRepository)
        {
            if (args.Length == ArgsCount)
            {
                if (args[2] != args[3])
                {
                    throw new Exception(Consts.Errors.PasswordsDoesntMatchErrorMessage);
                }

                return new RegistrationCommand(args, userRepository);
            }

            throw new ArgumentException(
                string.Format(Consts.Messages.InvalidCommandArgumentsMessage, nameof(RegistrationCommand), ArgsCount));
        }

        public override async Task ProcessMessage(SocketUser sender, SocketHandler socketHandler) //TODO Fix registration (update user in connectionManager)
        {
            if(sender.IsRegistered || sender.IsLoggedIn)
            {
                await socketHandler.SendMessageToYourself(new Message
                {
                    MessageText = string.Format(Consts.Errors.UserIsAlreadyRegisteredErrorMessage, sender.Nickname)
                }, sender.Id);

                return;
            };

            var oldNick = Args[0];

            sender.Nickname = Args[0];
            sender.Email = Args[1];
            sender.Password = Args[2];

            var result = await _userRepository.RegisterUser(sender);

            if(result.IsSuccessStatusCode)
            {
                sender.IsRegistered = true;
                sender.IsLoggedIn = true;

                var oldUserToDelete = socketHandler.ConnectionManager[oldNick, true];

                await socketHandler.ConnectionManager.RemoveSocket(oldUserToDelete);

                await socketHandler.SendMessageToYourself(
                    new Message
                    {
                        SenderNickname = sender.Nickname,
                        MessageText = string.Format(Consts.RegistrationCompletenessMessage, sender.Nickname) + Consts.CanLoginMessage,// + "\r\n" + "TOKEN [" + await result.Content.ReadAsStringAsync() + "] END OF TOKEN",
                        Settings = new MessageSettings()
                    }, sender.Id);

                await socketHandler.SendPublicMessage(new Message
                {
                    MessageText = string.Format(Consts.UserRegistrationMessage, sender.Nickname)
                }, sender.Id);
            }
        }
    }
}
