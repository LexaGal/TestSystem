using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace MvcTestSystem.Authentication
{
    public class PasswordCryptor : ICryptor
    {
        public string Encrypt(string text)
        {
            using (HashAlgorithm md5 = new SHA1CryptoServiceProvider())
            {
                byte[] data = Encoding.ASCII.GetBytes(text);
                byte[] result = md5.ComputeHash(data, 0, data.Count());
                return Convert.ToBase64String(result);
            }
        }
    }
}