using System;
using System.Threading.Tasks;
using WebSocketChatServerApp;

namespace WebSocketChatCoreLib.AdditionalClasses
{
    public static class ClearEntity
    {
        public async static Task<SocketUser> Clear(this SocketUser userToClear, SocketHandler socketHandler)
        {
            var newId = Guid.NewGuid();

            await socketHandler.ConnectionManager.RemoveSocket(userToClear.Id);
            socketHandler.ConnectionManager.ChangeSocketId(userToClear.Id, newId);

            userToClear.Email = string.Empty;
            userToClear.Id = newId;
            userToClear.IsConfirmed = false;
            userToClear.IsLoggedIn = false;
            userToClear.IsRegistered = false;
            userToClear.Nickname = NicknameGenerator.CreateNickname();
            userToClear.Password = string.Empty;
            userToClear.PhoneNumber = 0;
            userToClear.UserMessageSettings = new MessageSettings(MessageSettings.MessageSettingsPreset.UnregisteredSettings);
            userToClear.Token = string.Empty;

            return userToClear;
        }
    }
}
