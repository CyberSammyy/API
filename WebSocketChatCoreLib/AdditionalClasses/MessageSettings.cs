using System;

namespace WebSocketChatCoreLib.AdditionalClasses
{
    public class MessageSettings
    {
        public enum MessageSettingsPreset
        {
            DefaultSettings = 0,
            UnregisteredSettings = 1,
            CustomSettings = 2
        }

        private ConsoleColor messageColor;

        public MessageSettingsPreset Preset { get; set; } = MessageSettingsPreset.DefaultSettings;

        public ConsoleColor MessageColor
        {
            get
            {
                return messageColor;
            }
            set
            {
                if(Preset == MessageSettingsPreset.DefaultSettings)
                {
                    messageColor = ConsoleColor.White;
                }
                else if(Preset == MessageSettingsPreset.UnregisteredSettings)
                {
                    messageColor = ConsoleColor.Gray;
                }
                else if(Preset == MessageSettingsPreset.CustomSettings)
                {
                    try
                    {
                        messageColor = value;
                    }
#pragma warning disable CS0168 // Variable is declared but never used
                    catch (Exception ex)
#pragma warning restore CS0168 // Variable is declared but never used
                    {
                        messageColor = ConsoleColor.Gray;
                    }
                }
            }
        }

        public ConsoleColor ReceivedMessageColor { get; set; } = ConsoleColor.White;

        public MessageSettings() { }

        public MessageSettings(MessageSettings settings)
        {
            Preset = settings.Preset;
            MessageColor = settings.MessageColor;
            ReceivedMessageColor = settings.ReceivedMessageColor;
        }

        public MessageSettings(MessageSettingsPreset preset, 
            ConsoleColor color = ConsoleColor.Gray, 
            ConsoleColor receiverMessageColor = ConsoleColor.White)
        {
            Preset = preset;
            MessageColor = color;
            ReceivedMessageColor = receiverMessageColor;
        }
    }
}
