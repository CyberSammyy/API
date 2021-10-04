using System;
using WebSocketChatCoreLib.AdditionalClasses;
using WebSocketChatCoreLib.Commands.EncryptedChatCommands;
using WebSocketChatCoreLib.Commands.MessageCommands;
using WebSocketChatCoreLib.Commands.RegistrationAndAuthenticationCommands;
using WebSocketChatCoreLib.Commands.UtilityCommands;
using WebSocketChatCoreLib.Commands.VisualChangingCommands;
using WebSocketChatCoreLib.Commands.WebSocketChatCoreLib.Commands.RegistrationAndAuthenticationCommands;
using WebSocketChatCoreLib.Interfaces;

namespace WebSocketChatCoreLib.Commands
{
    public class CommandHelper
    {
        private static IUserRepository _userRepository;

        public CommandHelper(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Command GetCommand(string message)
        {
            Command result;
            if (!string.IsNullOrWhiteSpace(message))
            {
                bool isCommand = message.StartsWith(Consts.Commands.CommandSign);
                if (isCommand && message.Length > 1)
                {
                    message = message.Substring(1);
                    var commandArgs = message.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    var commandName = commandArgs[0];
                    commandArgs = commandArgs[1..];

                    result = commandName switch
                    {
                        string str when str.StartsWith(Consts.Commands.PrivateMessageCommand)
                            => PrivateMessageCommand.Create(commandArgs),
                        string str when str.StartsWith(Consts.Commands.NicknameChangeCommand)
                            => NicknameChangeCommand.Create(commandArgs, _userRepository),
                        string str when str.StartsWith(Consts.Commands.ColorChangeCommand)
                            => ColorChangeCommand.Create(commandArgs),
                        string str when str.StartsWith(Consts.Commands.RegistrationCommand)
                            => RegistrationCommand.Create(commandArgs, _userRepository),
                        string str when str.StartsWith(Consts.Commands.LoginCommand)
                            => LoginCommand.Create(commandArgs, _userRepository),
                        string str when str.StartsWith(Consts.Commands.GetUserIdCommand)
                            => GetUserIdCommand.Create(commandArgs),
                        string str when str.StartsWith(Consts.Commands.RemoveUserAdminCommand)
                            => RemoveUserAdminCommand.Create(commandArgs),
                        string str when str.StartsWith(Consts.Commands.LogoutCommand)
                            => LogoutCommand.Create(commandArgs),
                        string str when str.StartsWith(Consts.Commands.EncryptedChatRequestCommand)
                            => EncryptedChatRequestCommand.Create(commandArgs),
                        string str when str.StartsWith(Consts.Commands.EncryptedChatCancelCommand)
                            => EncryptedChatCancelCommand.Create(commandArgs),
                        string str when str.StartsWith(Consts.Commands.EncryptedChatDenyCommand)
                            => EncryptedChatDenyCommand.Create(commandArgs),
                        string str when str.StartsWith(Consts.Commands.EncryptedChatAcceptCommand)
                            => EncryptedChatAcceptCommand.Create(commandArgs),
                        _ => InvalidCommand.Create()
                    };
                }
                else if(!isCommand)
                {
                    result = MessageToAllCommand.Create(new[] { message });
                }
                else
                {
                    result = InvalidCommand.Create();
                }
            }
            else
            {
                result = InvalidCommand.Create();
            }

            return result;
        }
    }
}
