using System.Security.Cryptography;
using System.Text;

namespace Hotel.Core.Sercurity.Cryptography
{
    public class SHA256Helper
    {

        public static string Enscrypt(string inputstring)
        {
            using (var shA256 = SHA256.Create())
            {
                var hash = shA256.ComputeHash(Encoding.UTF8.GetBytes(inputstring));
                var stringBuilder = new StringBuilder();
                for (var index = 0; index < hash.Length; ++index)
                    stringBuilder.Append(hash[index].ToString("x2"));
                return stringBuilder.ToString();
            }
        }


    }
}
