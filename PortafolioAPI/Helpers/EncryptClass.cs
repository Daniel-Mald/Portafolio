using System.Security.Cryptography;
using System.Text;

namespace PortafolioAPI.Helpers
{
    public class EncryptClass
    {
        public string? Encrypt(string pass, string userName)
        {
            if (string.IsNullOrWhiteSpace(pass)) { return null; }
            if (string.IsNullOrWhiteSpace(userName)) { return null; }


            using(SHA512 sha = SHA512.Create())
            {
                byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes($"{userName}d{pass}c{userName.ToLower()}"));
                StringBuilder b = new();
                for (int i = 0; i < bytes.Length; i++)
                {
                    b.Append(bytes[i].ToString("x2"));
                }
                return b.ToString();
            }


        }
    }
}
