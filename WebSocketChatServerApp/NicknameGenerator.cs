using System;

namespace WebSocketChatServerApp
{
    public static class NicknameGenerator
    {
        private static Random random = new Random();
        public static string CreateNickname()
        {
            return $"{Patterns.Nicknames.Prefixes[random.Next(0, Patterns.Nicknames.Prefixes.Length - 1)]}{random.Next(1000, 9999)}";
        }
    }
}
