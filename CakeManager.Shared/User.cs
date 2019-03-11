﻿using System.ComponentModel.DataAnnotations;

namespace CakeManager.Shared
{
    public class User
    {
        [Required(ErrorMessage = "Enter a username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Enter a password")]
        public string Password { get; set; }
    }
}
