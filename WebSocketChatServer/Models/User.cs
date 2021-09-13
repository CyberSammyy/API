using System;

namespace WebSocketChatServer
{
    public class User
    {
        public Guid Id { get; set; }
        public string Nickname { get; set; }
        public int PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }

        public override string ToString()
        {
            return $"User: {Id} - {Nickname}, {Email}.";
        }
    }
}
