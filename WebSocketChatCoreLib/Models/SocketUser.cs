using System;
using System.Net.WebSockets;
using WebSocketChatCoreLib.AdditionalClasses;

namespace WebSocketChatCoreLib.Models
{
    public class SocketUser
    {
        public Guid Id { get; set; }
        public string Nickname { get; set; } = NicknameGenerator.CreateNickname();
        public string Email { get; set; }
        public MessageSettings UserMessageSettings { get; set; } = new MessageSettings(MessageSettings.MessageSettingsPreset.DefaultSettings);
        public WebSocket WebSocket { get; set; }
        public int PhoneNumber { get; set; }
        public bool IsConfirmed { get; set; } = false;
        public bool IsRegistered { get; set; } = false;
        public bool IsLoggedIn { get; set; } = false;
        public EncryptedChatSessionSettings EncryptedSessionSettings { get; set; } = new EncryptedChatSessionSettings();
        public string Password { get; set; }
        public string Token { get; set; }

        public SocketUser(WebSocket websocket)
        {
            Id = Guid.NewGuid();
            WebSocket = websocket;
        }

        public override string ToString()
        {
            return !string.IsNullOrEmpty(Nickname) ? Nickname : Id.ToString(Consts.IdFormat);
        }
    }
}
