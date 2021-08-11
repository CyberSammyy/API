using System;
using System.Text;

namespace BusinessLogic.HelperClasses
{
    public static class StringGenerator
    {
        private static readonly Random _random;

        static StringGenerator()
        {
            _random = new Random();
        }

        public static string GenerateString(int minLength = 1, int maxLength = 20)
        {
            return "linkToRegister";
            var messageLength = _random.Next(minLength, maxLength);
            var generatedString = new StringBuilder(string.Empty);
            for (int i = 0; i < messageLength; i++)
            {
                generatedString.Append(GetRandomChar());
            }

            return generatedString.ToString();
        }

        private static char GetRandomChar()
        {
            char result = ' ';
            switch (_random.Next(1, 3))
            {
                case 1:
                    result = (char)_random.Next('A', 'Z');
                    break;
                case 2:
                    result = (char)_random.Next('a', 'z');
                    break;
                case 3:
                    result = (char)_random.Next('0', '9');
                    break;
            }

            return result;
        }
    }
}
