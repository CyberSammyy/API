using System;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace WebSocketChatCoreLib.Models
{
    public abstract class SocketHandler
    {
        public ConnectionManager ConnectionManager { get; set; }

        public SocketHandler(ConnectionManager connectionManager)
        {
            ConnectionManager = connectionManager;
        }

        public virtual async Task OnConnected(WebSocket socket)
            => await Task.Run(()
                => ConnectionManager.AddSocket(socket));

        public virtual async Task OnDisconnected(WebSocket socket)
            => await ConnectionManager.RemoveSocket(ConnectionManager.GetId(socket));

        public async Task<bool> SendMessage(WebSocket sender, Message messageToSend)
        {
            if(sender.State == WebSocketState.Open)
            {
                try
                {
                    var encodedMessage = Encoding.Unicode.GetBytes(JsonSerializer.Serialize(messageToSend));

                    var json = Encoding.Unicode.GetString(encodedMessage, 0, encodedMessage.Length);
                    var messageContract = JsonSerializer.Deserialize<Message>(json);

                    await sender.SendAsync(
                        new ArraySegment<byte>(encodedMessage, 0, encodedMessage.Length),
                        WebSocketMessageType.Text, true, CancellationToken.None);

                    return true;
                }
#pragma warning disable CS0168 // Variable is declared but never used
                catch (Exception ex)
#pragma warning restore CS0168 // Variable is declared but never used
                {
                    return false;
                }
            }

            return false;
        }

        public async Task<bool> SendMessageToYourself(Message messageToSend, Guid senderId)
        {
            foreach (var user in ConnectionManager)
            {
                if (senderId == user.Id)
                {
                    messageToSend.Settings.MessageColor = user.UserMessageSettings.MessageColor;
                    try
                    {
                        return await SendMessage(user.WebSocket, messageToSend);
                    }
#pragma warning disable CS0168 // Variable is declared but never used
                    catch (Exception ex)
#pragma warning restore CS0168 // Variable is declared but never used
                    {
                        return false;
                    }
                }
            }

            return false;
        }

        public async Task<bool> SendPublicMessage(Message messageToSend, Guid senderId)
        {
            foreach(var user in ConnectionManager)
            {
                if(senderId != user.Id)
                {
                    messageToSend.Settings.MessageColor = user.UserMessageSettings.MessageColor;
                    try
                    {
                        return await SendMessage(user.WebSocket, messageToSend);
                    }
#pragma warning disable CS0168 // Variable is declared but never used
                    catch (Exception ex)
#pragma warning restore CS0168 // Variable is declared but never used
                    {
                        return false;
                    }
                }
            }

            return false;
        }

        public abstract Task Receive(WebSocket sender, WebSocketReceiveResult result, byte[] messageBuffer);
    }
}
