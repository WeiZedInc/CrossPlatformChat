﻿using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace CrossPlatformChat.Services
{
    public static class CryptoManager // todo
    {
        public static (string HashedPassword, byte[] Salt) CreateHashedPassword(string password, string keyWordSalt)
        {
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create()) rng.GetBytes(salt);

            string hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password,
                salt,
                KeyDerivationPrf.HMACSHA1,
                10000,
                256 / 8));

            return (hashedPassword, salt);
        }

        public static bool VerifyPassword(string enteredPassword, byte[] storedSlat, string storedPassword)
        {
            string encryptedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(enteredPassword, storedSlat, KeyDerivationPrf.HMACSHA1, 10000, 256 / 8));
            return encryptedPassword.Equals(storedPassword);
        }
    }
}
