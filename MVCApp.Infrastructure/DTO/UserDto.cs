﻿using System;

namespace MVCApp.Infrastructure.DTO
{
    public class UserDto
    {
        public Guid UserId { get; set; }
        public string Ign { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}