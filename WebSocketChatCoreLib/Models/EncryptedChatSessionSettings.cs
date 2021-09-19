using RSACryptoServiceNetCore;
using System;

namespace WebSocketChatCoreLib.Models
{
    public class EncryptedChatSessionSettings
    {
        public RSAImpl RsaProvider { get; set; } = new RSAImpl();
        public Guid IncomingRequestingId { get; set; } = Guid.Empty;
        public Guid OutcomingRequestingId { get; set; } = Guid.Empty;
        public bool IsEncryptedChatRequestSent { get; set; } = false;
        public bool IsEncryptedChatRequestTaken { get; set; } = false;
        public bool IsEncryptedSessionStarted { get; set; } = false;
    }
}
