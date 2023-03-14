﻿using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CrossPlatformChat.Services
{
    internal class CryptoManager
    {
        (string HashedPassword, byte[] Salt) CreateHashedPassword(string password)
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
        bool VerifyPassword(string enteredPassword, byte[] storedSlat, string storedPassword)
        {
            string encryptedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(enteredPassword, storedSlat, KeyDerivationPrf.HMACSHA1, 10000, 256 / 8));
            return encryptedPassword.Equals(storedPassword);
        }
    }
}
