namespace DistributedModels
{
    public class MessageStringWithPublicKey
    {
        public string Message { get; set; }
        public string PublicKey { get; set; }
        public string SenderNickname { get; set; }

        public MessageStringWithPublicKey(string message, string senderNickname, string publicKey = null)
        {
            Message = message;
            SenderNickname = senderNickname;
            publicKey ??= PublicKey = string.Empty;
        }

        public MessageStringWithPublicKey() { }
    }
}
