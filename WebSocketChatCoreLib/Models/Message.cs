namespace WebSocketChatServerApp
{
    public class Message
    {
        public MessageSettings Settings { get; set; } = new MessageSettings(MessageSettings.MessageSettingsPreset.DefaultSettings);
        public string MessageText { get; set; }
        public string SenderNickname { get; set; }
    }
}
