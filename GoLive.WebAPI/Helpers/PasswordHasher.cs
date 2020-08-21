using System;
using System.Security.Cryptography;

namespace GoLive.Helpers
{
    public class PasswordHasher
    {
        private readonly RNGCryptoServiceProvider _rng;
        public PasswordHasher()
        {
            _rng = new RNGCryptoServiceProvider();
        }
        public string HashPassword(string password)
        {
            byte[] salt;
            _rng.GetBytes(salt = new byte[16]);
            
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000);
            byte[] hash = pbkdf2.GetBytes(20);

            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            return Convert.ToBase64String(hashBytes);
        }

        public bool ValidatePassword(string passwordHash, string password)
        {
            byte[] hashBytes = Convert.FromBase64String(passwordHash);
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000);
            byte[] hash = pbkdf2.GetBytes(20);

            for (int i=0; i < 20; i++)
            {
                if (hashBytes[i+16] != hash[i])
                    return false;
            }

            return true;
        }
    }
}