// File: Services/UserService.cs
using System;
using System.Collections.Generic;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using MediTechDesktopApp.DataAccess;  // MySqlDbHelper

namespace MediTechDesktopApp.Services
{
    public class UserService
    {
        private readonly MySqlDbHelper _db;

        public UserService()
        {
            _db = new MySqlDbHelper();
        }

        /// <summary>
        /// Validates the given username/password against the users table.
        /// Assumes table `users` has columns: (user_id, username, password_hash, role)
        /// and that password_hash = SHA256(plaintext_password).
        /// Returns true only if the username exists and the hash matches.
        /// </summary>
        public bool ValidateUser(string username, string password)
        {
            // 1) Fetch the stored hash for that username
            string sql = @"
                SELECT password_hash
                  FROM users
                 WHERE username = @username
                LIMIT 1;
            ";
            var parms = new Dictionary<string, object>
            {
                { "username", username }
            };

            DataTable dt = _db.ExecuteRawQuery(sql, parms);
            if (dt.Rows.Count == 0)
            {
                // No user found
                return false;
            }

            string storedHash = dt.Rows[0]["password_hash"].ToString();

            // 2) Hash the provided password with SHA-256
            string computedHash = ComputeSha256Hash(password);

            // 3) Compare (case-insensitive)
            return string.Equals(storedHash, computedHash, StringComparison.OrdinalIgnoreCase);
        }

        private static string ComputeSha256Hash(string raw)
        {
            using (var sha = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(raw);
                byte[] hash = sha.ComputeHash(bytes);
                var sb = new StringBuilder();
                foreach (byte b in hash)
                    sb.Append(b.ToString("x2")); // lowercase hex
                return sb.ToString();
            }
        }
    }
}
