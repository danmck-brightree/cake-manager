using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CakeManager.Repository.Models
{
    public class TempUser
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public Guid OfficeId { get; set; }

        public virtual Office Office { get; set; }

        public virtual List<CakeMark> CakeMarks { get; set; }
        public virtual List<SuperCakeMark> SuperCakeMarks { get; set; }
    }
}
