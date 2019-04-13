﻿using System.ComponentModel.DataAnnotations;

namespace MVCApp.Presentation.Models
{
    public class LoginViewModel
    {
        [Required (ErrorMessage = "Missing email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required (ErrorMessage = "Missing password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}