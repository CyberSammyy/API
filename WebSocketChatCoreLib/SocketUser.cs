using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;

namespace WebSocketChatServerApp
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
