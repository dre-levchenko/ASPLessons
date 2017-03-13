using System.Security.Cryptography;
using System.Text;

namespace Gallery.Models.Security
{
    static class MD5Hasher
    {
        static public string ComputeHash(string input)
        {
            byte[] hash;
            byte[] inputBytes = Encoding.ASCII.GetBytes(input);

            using (MD5 md5 = MD5.Create())
            {
                hash = md5.ComputeHash(inputBytes);
            }

            var stringBuilder = new StringBuilder();
            for (var index = 0; index < hash.Length; ++index)
            {
                stringBuilder.Append(hash[index].ToString("x2"));
            }

            return stringBuilder.ToString();
        }
    }
}
