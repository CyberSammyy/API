using System;

namespace DataAccess.Models
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string Nickname { get; set; }
        public int PhoneNumber { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        public override string ToString()
        {
            return $"User: {Id} - {Nickname}, {Email}.";
        }
    }
}
