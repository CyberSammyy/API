using System;
using System.Text;

namespace BusinessLogic.HelperClasses
{
    public static class ByteHelper
    {
        public static string ByteArrayToString(byte[] array)
        {
            var stringBuilder = new StringBuilder(string.Empty);

            foreach (var item in array)
            {
                stringBuilder.Append($"{item}_");
            }

            stringBuilder = stringBuilder.Remove(stringBuilder.Length - 1, 1);

            return stringBuilder.ToString();
        }

        public static byte[] StringToByteArray(string stringWithBytes)
        {
            var encryptedBytesString = stringWithBytes.Split('_');
            byte[] result = new byte[encryptedBytesString.Length];
            for (int i = 0; i < encryptedBytesString.Length; i++)
            {
                if (!byte.TryParse(encryptedBytesString[i], out result[i]))
                {
                    throw new Exception();
                }
            }

            return result;
        }
    }
}
