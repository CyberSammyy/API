using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebSocketChatServerApp
{
    public static class Consts
    {
        public static class Messages
        {
            public const string InvalidCommandMessage = "Invalid command!";
            public const string NicknameChangedMessage = "{0} nickname changed to {1}";
            public const string ConnectionClosedMessage = "Socket connection closed";
            public const string JoinMessage = "{0} just joined the party *****";
            public const string LeaveMessage = "{0} just left the party *****";
            public const string InvalidCommandArgumentsMessage = "Command \"{0}\" required {1} args.";
        }

        public static class ExceptionMessages
        {
            public const string NullReferenceExceptionMessage = "Entity <{0}> wasn't found in {1}. Searching by {2}";
        }

        public static class Commands
        {
            public const char CommandSign = '/';
            public const string PrivateMessageCommand = "msg";
            public const string NicknameChangeCommand = "nickname";
            public const string ColorChangeCommand = "color";
            public const string RegistrationCommand = "register";
            public const string LoginCommand = "login";
        }

        public const string PrivateMessageFormat = "{0} => {1}";
        public const string IdFormat = "N";
        public const int MessageSizeInBytes = 1024 * 4;
        public const string ColorChangeMessage = "Color changed!";
        public const string RegistrationCompletenessMessage = "Has been registered successfully!";
    }
}
