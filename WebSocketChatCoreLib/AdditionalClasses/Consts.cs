namespace WebSocketChatServerApp
{
    public static class Consts
    {
        public static class Messages
        {
            public const string InvalidCommandMessage = "Invalid command!";
            public const string NicknameChangedMessage = "{0} nickname changed to {1}";
            public const string NicknameChangedMessageToYourself = "You have successfully changed your nickname to {0}";
            public const string ConnectionClosedMessage = "Socket connection closed";
            public const string JoinMessage = "{0} just joined the party *****";
            public const string LeaveMessage = "{0} just left the party *****";
            public const string InvalidCommandArgumentsMessage = "Command \"{0}\" required {1} args.";
        }

        public static class Errors
        {
            public const string NotRegisteredUserErrorMessage = "You are not registered user! Some features are disabled. \r\n Please, complete your registration by command /register Nickname Email Password Password. \r\n If have account, you can try to login by typing /login Nickname Password";
            public const string NotConfirmedUserErrorMessage = "\r\n Additional condition for this command: you have to confirm your email address. \r\nPlease, check your mail and type /confirm.";
            public const string PasswordsDoesntMatchErrorMessage = "Passwords doesn't match!";
            public const string NotEnoughPermissionsErrorMessage = "Your permission isn't enough to perform this command!";
            public const string UserIsAlreadyLoggedInErrorMessage = "You are logged in already as {0}. You can log out by typing /logout";
            public const string UserIsAlreadyRegisteredErrorMessage = "You are registered already as {0}. You can log out by typing /logout";
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
            public const string RemoveUserAdminCommand = "us-rm-adm";
            public const string GetUserIdCommand = "id";
        }

        public const string PrivateMessageFormat = "{0} => {1}";
        public const string IdFormat = "N";
        public const int MessageSizeInBytes = 1024 * 4;
        public const string ColorChangeMessage = "Color changed!";
        public const string RegistrationCompletenessMessage = "Has been registered successfully!";
        public const string LoginCompletenessMessage = "You have been logged in as {0}";
        public const string UserLoginMessage = "{0} logged in! Yikes!";
        public const string UserRegistrationMessage = "{0} successfully registered!";
    }
}
