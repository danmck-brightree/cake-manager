using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CakeManager.Repository.Models
{
    public class ActiveDirectoryUser
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public string Email { get; set; }

        public Guid? OfficeId { get; set; }

        public virtual Office Office { get; set; }

        public virtual List<CakeMark> CakeMarks { get; set; }

        public virtual List<SuperCakeMark> SuperCakeMarks { get; set; }
    }
}
