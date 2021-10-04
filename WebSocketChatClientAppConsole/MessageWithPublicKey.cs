using WebSocketChatCoreLib.Models;

namespace WebSocketChatClientAppConsole
{
    public class MessageWithPublicKey
    {
        public string PublicKey { get; set; }
        public Message Message { get; set; }

        public MessageWithPublicKey(Message message, string publicKey = null)
        {
            publicKey ??= PublicKey = string.Empty;
            Message = message;
        }

        public MessageWithPublicKey() { }
    }
}
