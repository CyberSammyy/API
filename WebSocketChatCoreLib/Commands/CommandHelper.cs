using System;
using WebSocketChatCoreLib;
using WebSocketChatCoreLib.Commands;

namespace WebSocketChatServerApp.Commands
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
