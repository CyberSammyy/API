using System;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using WebSocketChatCoreLib.AdditionalClasses;
using WebSocketChatCoreLib.Models;

namespace WebSocketChatClientAppConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Run().GetAwaiter().GetResult();
        }

        public static async Task Run()
        {
            var client = new ClientWebSocket();
            await client.ConnectAsync(new Uri("ws://localhost:5000/ws"), CancellationToken.None);
            Console.WriteLine($"Web socket connection established @ {DateTime.UtcNow:F}");
            var send = Task.Run(async () =>
            {
                string message;
                while (!string.IsNullOrEmpty(message = Console.ReadLine()))
                {
                    var bytes = Encoding.Unicode.GetBytes(message);
                    await client.SendAsync(
                        new ArraySegment<byte>(bytes),
                        WebSocketMessageType.Text,
                        true,
                        CancellationToken.None);
                }

                await client.CloseOutputAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);
            });
        
            var receive = ReceiveAsync(client);
            await Task.WhenAll(send, receive);
        }

        public static async Task ReceiveAsync(ClientWebSocket client)
        {
            var buffer = new byte[Consts.MessageSizeInBytes];
            WebSocketReceiveResult result;
            do
            {
                result = await client.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

                var json = Encoding.Unicode.GetString(buffer, 0, result.Count);
                var messageContract = JsonSerializer.Deserialize<Message>(json);

                ProcessMessage(messageContract);
            } while (result.MessageType != WebSocketMessageType.Close);
        }

        private static void ProcessMessage(Message message)
        {
            if (message.SenderNickname == null || message.Settings == null)
            {
                message.SenderNickname = string.Empty;
                message.Settings = new MessageSettings(MessageSettings.MessageSettingsPreset.DefaultSettings);
            }

            Console.ForegroundColor = message.Settings.ReceivedMessageColor;
            if(message.SenderNickname.Length == 0 || 
                message.SenderNickname == string.Empty ||
                message.SenderNickname == "")
            {
                Console.WriteLine($"{message.MessageText}");
            }
            else
            {
                Console.WriteLine($"{message.SenderNickname} at {DateTime.Now:g} said: {message.MessageText}");
                Console.ForegroundColor = message.Settings.MessageColor;
            }
        }
    }
}
