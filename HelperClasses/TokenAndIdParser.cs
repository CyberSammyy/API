using System;

namespace HelperClasses
{
    public static class TokenAndIdParser
    {
        public static (string token, Guid id, string email, string phoneNumber) ParseTokenIdString(string tokenAndId)
        {
            var parts = tokenAndId.Split(HelperConstants.UNIVERSAL_RESPONSE_STRING_SEPARATOR);

            return (parts[0], Guid.Parse(parts[1]), parts[2], parts[3]);
        }

        public static string RemoveIdFromTokenIdString(string tokenAndId)
        {
            tokenAndId = tokenAndId.Replace(tokenAndId.Substring(tokenAndId.IndexOf(HelperConstants.UNIVERSAL_RESPONSE_STRING_SEPARATOR)), string.Empty).Trim();
            
            return tokenAndId;
        }
    }
}
