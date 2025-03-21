﻿using System.ComponentModel.DataAnnotations;

namespace PhonebookSystem.Core.Application.Abstraction.DTOs.Auth
{
    public class RegisterDTO
    {
        [Required]
        public required string UserName { get; set; }

        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        public required string Password { get; set; }
    }
}
