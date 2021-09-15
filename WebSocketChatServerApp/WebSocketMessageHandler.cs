using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using WebSocketChatServerApp.Commands;

namespace WebSocketChatServerApp
{
    public class WebSocketMessageHandler : SocketHandler
    {
        private readonly CommandHelper _commandHelper;
        public WebSocketMessageHandler(CommandHelper commandHelper, ConnectionManager socketUsers) : base(socketUsers)
        {
            _commandHelper = commandHelper;
        }

        public override async Task<bool> OnConnected(WebSocket socket)
        {
            await base.OnConnected(socket);

            var socketUser = ConnectionManager[socket];

            return await SendPublicMessage(new Message
            {
                MessageText = Consts.Messages.JoinMessage + "Registration status: " + socketUser.IsRegistered,
                SenderNickname = socketUser.Nickname,
                Settings = new MessageSettings(MessageSettings.MessageSettingsPreset.DefaultSettings)
            }, socketUser.Id);
        }

        public override async Task<bool> OnDisconnected(WebSocket socket)
        {
            await base.OnDisconnected(socket);

            var socketUser = ConnectionManager[socket];

            return await SendPublicMessage(new Message
            {
                MessageText = Consts.Messages.LeaveMessage,
                SenderNickname = socketUser.Nickname,
                Settings = socketUser.UserMessageSettings
            }, socketUser.Id);
        }
        public override async Task Receive(WebSocket sender, WebSocketReceiveResult result, byte[] messageBuffer)
{
            var webSocketClient = ConnectionManager[sender];
            var messageText = Encoding.Unicode.GetString(messageBuffer, 0, result.Count);
            await ProcessMessage(webSocketClient, messageText);
        }

        private async Task ProcessMessage(SocketUser senderSocket, string messageFromClient)
        {
            var command = _commandHelper.GetCommand(messageFromClient);

            await command?.ProcessMessage(senderSocket, this);
        }
    }
}
