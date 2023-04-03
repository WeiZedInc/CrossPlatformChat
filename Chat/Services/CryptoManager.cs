using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Maui;
using System.Security.Cryptography;

namespace CrossPlatformChat.Services
{
    public static class CryptoManager // todo
    {
        public static (string Hash, byte[] Salt) CreateHash(string key)
        {
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create()) rng.GetBytes(salt);

            string hash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                key,
                salt,
                KeyDerivationPrf.HMACSHA256,
                10000,
                256 / 8));

            return (hash, salt);
        }

        public static bool VerifyHash(string enteredKey, byte[] storedSalt, string hashToVerify)
        {
            string encryptedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                enteredKey,
                storedSalt,
                KeyDerivationPrf.HMACSHA256,
                10000,
                256 / 8));

            return encryptedPassword.Equals(hashToVerify);
        }
    }
}
