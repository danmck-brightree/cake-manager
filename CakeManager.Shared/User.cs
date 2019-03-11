using System;
using System.ComponentModel.DataAnnotations;

namespace CakeManager.Shared
{
    public class User
    {
        public static readonly Guid TemporaryUserId = new Guid("41fc2054-1e96-47b9-8faf-6069810b9b2f");

        public Guid? Id { get; set; }

        [Required(ErrorMessage = "Enter a username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Enter a password")]
        public string Password { get; set; }
    }
}
