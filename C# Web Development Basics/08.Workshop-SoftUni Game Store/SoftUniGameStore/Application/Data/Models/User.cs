﻿namespace SoftUniGameStore.Application.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Common;

    public class User
    {
        public int Id { get; set; }

        [MinLength(ValidationConstants.Account.NameMinLength)]
        [MaxLength(ValidationConstants.Account.NameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MinLength(ValidationConstants.Account.PasswordMinLength)]
        [MaxLength(ValidationConstants.Account.PasswordMaxLength)]
        public string Password { get; set; }

        [Required]
        [MaxLength(ValidationConstants.Account.EmailMaxLength)]
        public string Email { get; set; }

        public bool IsAdmin { get; set; }

        public List<UserGame> Games { get; set; } = new List<UserGame>();
    }
}
