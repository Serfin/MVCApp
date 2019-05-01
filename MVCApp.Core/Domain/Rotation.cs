using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using MVCApp.Core.Enums;

namespace MVCApp.Core.Domain
{
    public class Rotation
    {
        private ISet<User> _members = new HashSet<User>();
        public Rotation(Guid rotationId, string creator, LeagueName league, RotationType type, int? spots)
        {
            SetRotationId(rotationId);
            SetCreator(creator);
            SetLeague(league);
            SetType(type);
            SetSpots(spots);
            CreatedAt = DateTime.Now;
        }

        protected Rotation()
        {

        }

        // Entity Framework one-to-one relation class
        public User User { get; set; }
        [ForeignKey("User")]
        public string Creator { get; protected set; }

        public Guid RotationId { get; protected set; }
        public string League { get; protected set; }
        public string Type { get; protected set; }
        public int? Spots { get; protected set; }

        public virtual ICollection<User> Members
        {
            get => _members;
            protected set => _members = new HashSet<User>();
        }
        public DateTime CreatedAt { get; protected set; }

        private void SetRotationId(Guid rotationId)
        {
            if (rotationId == null)
                throw new ArgumentNullException($"{nameof(rotationId)} cannot be null");

            RotationId = rotationId;
        }

        private void SetCreator(string creator)
        {
            Creator = creator ?? throw new Exception("Rotation must contain creator");
        }

        private void SetType(RotationType type)
        {
            if (string.IsNullOrWhiteSpace(type.ToString()))
            {
                throw new Exception("Type cannot be empty");
            }

            Type = type.ToString();
        }

        private void SetLeague(LeagueName league)
        {
            if (string.IsNullOrWhiteSpace(league.ToString()))
            {
                throw new Exception("League name cannot be empty");
            }

            League = league.ToString();
        }

        private void SetSpots(int? spots)
        {
            if (spots == null)
            {
                throw new Exception("Number of spots cannot be empty");
            }
            if (spots < 1)
            {
                throw new Exception("Number of spots must be greater than 0");
            }
            if (spots > 5)
            {
                throw new Exception("Number of spots cannot be greater than 5");
            }

            Spots = spots;
        }

        public void AddMember(User member)
        {
            if (member == null)
            {
                throw new Exception("Cannot add empty user");
            }
            if (Spots == 0)
            {
                throw new Exception("No spots left");
            }
            if (_members.Contains(member))
            {
                throw new Exception("User with this ID is already in this rotation");
            }
            if (member.Ign == Creator)
            {
                throw new Exception("User cannot join his own rotation");
            }

            _members.Add(member);
            Spots = Spots - 1;
        }

        public void DeleteMember(User member)
        {
            if (member == null)
            {
                throw new Exception("Cannot delete empty user");
            }
            if (!_members.Contains(member))
            {
                throw new Exception("User does not exist in this rotation");
            }
            if (member.Ign == Creator)
            {
                throw new Exception("User cannot be from his own rotation");
            }

            _members.Remove(member);
            Spots = Spots + 1;
        }
    }
}