using hr.makemystamp.com.application.Interfaces;
using System.Security.Cryptography;

namespace hr.makemystamp.com.core.Service
{
    public class PasswordService : IPasswordService
    {
        private const int SaltSize = 16; // 16 bytes = 128 bits
        private const int HashSize = 32; // 32 bytes = 256 bits
        private const int Iterations = 10000; // PBKDF2 iteration count

        public (string Password, string Salt, string HashedPassword) HashPassword(string password)
        {
            // Generate a cryptographically secure random salt
            byte[] saltBytes = new byte[SaltSize];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(saltBytes);
            }
            string salt = Convert.ToBase64String(saltBytes);

            // Hash the password using PBKDF2
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, Iterations, HashAlgorithmName.SHA256))
            {
                byte[] hash = pbkdf2.GetBytes(HashSize);
                string hashedPassword = Convert.ToBase64String(hash);

                return (password, salt, hashedPassword); // Returning all values
            }
        }

        public bool VerifyPassword(string password, string storedSalt, string storedHash)
        {
            byte[] saltBytes = Convert.FromBase64String(storedSalt);
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, Iterations, HashAlgorithmName.SHA256))
            {
                byte[] hash = pbkdf2.GetBytes(HashSize);
                return storedHash == Convert.ToBase64String(hash); // Compare hashes
            }
        }
    }
}