using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Maui;
using System.Security.Cryptography;

namespace CrossPlatformChat.Services
{
    public static class CryptoManager
    {
        /// <summary>
        /// Creates a Tuple of (string Hash, byte[] Salt) depending on the given key.
        /// (Remember key and store salt to be able to encrypt hash later)
        /// </summary>
        public static (string Hash, byte[] Salt) CreateHash(string keyWord)
        {
            var salt = CreateSalt(keyWord);

            string hash = Convert.ToBase64String(salt);

            return (hash, salt);
        }

        public static byte[] CreateSalt(string keyWord)
        {
            byte[] salt = new byte[32]; // 256-bit
            using (var rng = RandomNumberGenerator.Create()) rng.GetBytes(salt);

            return KeyDerivation.Pbkdf2(keyWord, salt, KeyDerivationPrf.HMACSHA256, 10000, 32);
        }

        public static bool VerifyHash(string hashToVerify, string keyWord, byte[] storedSalt)
        {
            string encryptedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                keyWord,
                storedSalt,
                KeyDerivationPrf.HMACSHA256,
                10000,
                32));

            return encryptedPassword.Equals(hashToVerify);
        }

        #region Message encryption


        //Code below is a modified version of the sample code provided in the Microsoft documentation for the Aes class.
        //The Aes class is a part of the System.Security.Cryptography namespace in .NET Framework
        //and provides a managed implementation of the Advanced Encryption Standard (AES) algorithm,
        //which is a symmetric encryption algorithm widely used for encrypting sensitive data.

        public static (byte[] encryptedMessage, byte[] initialVector) EncryptMessage(byte[] salt, string message = "Hello world!")
        {
            byte[] initialVector = new byte[16]; // 128-bit 
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(initialVector);
            }

            return (Encrypt(message, salt, initialVector), initialVector);
        }

        static byte[] Encrypt(string oiriginalText, byte[] salt, byte[] initialVector)
        {
            if (oiriginalText == null || oiriginalText.Length <= 0)
                return null;

            if (salt == null || salt.Length <= 0)
                return null;

            if (initialVector == null || initialVector.Length <= 0)
                return null;

            byte[] encrypted;

            // Create an AES object with the specified key and IV
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = salt;
                aesAlg.IV = initialVector;

                // Create an encryptor to perform the stream transform
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption
                using var msEncrypt = new MemoryStream();
                using var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);
                using (var swEncrypt = new StreamWriter(csEncrypt))
                {
                    swEncrypt.Write(oiriginalText);
                }
                encrypted = msEncrypt.ToArray();
            }

            return encrypted;
        }

        public static string DecryptMessage(byte[] encryptedText, byte[] salt, byte[] initialVector)
        {
            if (encryptedText == null || encryptedText.Length <= 0)
                return null;

            if (salt == null || salt.Length <= 0)
                return null;

            if (initialVector == null || initialVector.Length <= 0)
                return null;

            string originalText;

            // Create an AES object with the specified key and IV
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = salt;
                aesAlg.IV = initialVector;

                // Create a decryptor to perform the stream transform
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for decryption
                using var msDecrypt = new MemoryStream(encryptedText);
                using var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
                using var srDecrypt = new StreamReader(csDecrypt);
                originalText = srDecrypt.ReadToEnd();
            }

            return originalText;
        }
        #endregion
    }
}
