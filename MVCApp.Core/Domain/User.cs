using System;
using System.Collections.Generic;
using ExileRota.Core.Domain;

namespace MVCApp.Core.Domain
{
    public class User
    {
        public User(Guid userId, string email, string ign, string password,
            string salt, string role)
        {
            SetUserId(userId);
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

        // Entity Framework many-to-many relation 
        public virtual ICollection<Rotation> Rotations { get; set; }

        private void SetUserId(Guid userId)
        {
            if (userId == null)
            {
                throw new ArgumentNullException($"{nameof(userId)} cannot be empty");
            }

            if (UserId != null)
            {
                throw new ArgumentException("UserID cannot be modified!");
            }

            UserId = userId;
        }

        private void SetEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentNullException($"{nameof(email)} cannot be empty");
            }

            Email = email;
        }

        private void SetIgn(string ign)
        {
            if (string.IsNullOrWhiteSpace(ign))
            {
                throw new ArgumentNullException($"{nameof(ign)} cannot be empty");
            }

            if (Ign != null)
            {
                throw new ArgumentException("User IGN cannot be modified!");
            }

            if (ign.Length < 3)
            {
                throw new ArgumentException("Ign must contain at least 3 characters in length");
            }

            Ign = ign;
        }

        private void SetRole(string role)
        {
            if (string.IsNullOrWhiteSpace(role))
            {
                throw new ArgumentNullException($"{nameof(role)} cannot be empty");
            }

            Role = role;
        }

        private void SetPassword(string password, string salt)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentNullException($"{nameof(password)} cannot be empty");
            }
            if (password.Length < 6)
            {
                throw new ArgumentException("Password must contain at least 6 characters in length");
            }
            if (password.Length > 100)
            {
                throw new ArgumentException("Password cannot contain more than 20 characters in length");
            }
            if (string.IsNullOrWhiteSpace(salt))
            {
                throw new ArgumentNullException($"{nameof(salt)} cannot be empty");
            }

            Password = password;
            Salt = salt;
        }

        public void SetUpdateTime(DateTime updatedAt)
            => UpdatedAt = updatedAt;
    }
}