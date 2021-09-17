using System.Security.Cryptography;
using System.Text;

namespace MD5Generator
{
    public static class MD5Generator
    {
        public static string GetMD5Hash(this string text)
        {
            using (var hashAlg = MD5.Create())
            {
                byte[] hash = hashAlg.ComputeHash(Encoding.UTF8.GetBytes(text));
                var builder = new StringBuilder(hash.Length * 2);

                for (int i = 0; i < hash.Length; i++)
                {
                    builder.Append(hash[i].ToString("X2"));
                }

                return builder.ToString();
            }
        }
    }
}
