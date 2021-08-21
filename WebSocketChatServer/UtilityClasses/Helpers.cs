using System;

namespace WebSocketChatServer
{
    public static class Helpers
    {
        public static string FailMessage(Exception ex)
        {
            return $"Something went wrong. Details: {ex.Message}";
        }
    }
}
