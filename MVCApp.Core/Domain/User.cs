using System;
using System.Collections.Generic;

namespace MVCApp.Core.Domain
{
    public class User
    {
        public User(Guid userId, string email, string ign, string password,
            string salt, string role)
        {
            UserId = userId;
            SetEmail(email);
            SetIgn(ign);
            SetPassword(password, salt);
            SetIgn(ign);
            SetRole(role);
            CreatedAt = DateTime.Now;
        }

        protected User()
        {
            
        }

        public Guid UserId { get; protected set; }
        public string Email { get; protected set; }
        public string Ign { get; protected set; }
        public string Password { get; protected set; }
        public string Salt { get; protected set; }
        public string Role { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime UpdatedAt { get; protected set; }

        private void SetEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new Exception("Email cannot be empty");
            }

            Email = email;
        }

        private void SetIgn(string ign)
        {
            if (string.IsNullOrWhiteSpace(ign))
            {
                throw new Exception("Ign cannot be empty");
            }

            if (ign.Length < 3)
            {
                throw new Exception("Ign must contain at least 3 characters in length");
            }

            Ign = ign;
        }

        private void SetRole(string role)
        {
            if (string.IsNullOrWhiteSpace(role))
            {
                throw new Exception("Role cannot be empty");
            }

            Role = role;
        }

        private void SetPassword(string password, string salt)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new Exception("Password cannot be empty");
            }
            if (password.Length < 6)
            {
                throw new Exception("Password must contain at least 6 characters in length");
            }
            if (password.Length > 100)
            {
                throw new Exception("Password cannot contain more than 20 characters in length");
            }
            if (string.IsNullOrWhiteSpace(salt))
            {
                throw new Exception("Salt cannot be empty");
            }

            Password = password;
            Salt = salt;
        }
    }
}