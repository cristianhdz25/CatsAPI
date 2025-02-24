using System;
using System.Security.Cryptography;

namespace CatAPI.BC.Utilities
{
    public class PasswordHasher
    {
        // Hash the password using HMACSHA512 and return the hash and salt.
        public (byte[] Hash, byte[] Salt) HashPassword(string password)
        {
            using var hmac = new HMACSHA512();  // Create an instance of HMACSHA512.
            var salt = hmac.Key;  // The key used by HMACSHA512 is used as the salt.
            var hash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));  // Compute the hash of the password.
            return (hash, salt);  // Return the computed hash and the salt used.
        }

        // Verify the password by comparing the computed hash with the stored hash.
        public bool VerifyPassword(string password, byte[] storedHash, byte[] storedSalt)
        {
            using var hmac = new HMACSHA512(storedSalt);  // Initialize HMACSHA512 with the stored salt.
            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));  // Compute the hash for the input password.
            return computedHash.SequenceEqual(storedHash);  // Compare the computed hash with the stored hash.
        }

    }
}
