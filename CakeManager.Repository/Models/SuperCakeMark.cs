using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CakeManager.Repository.Models
{
    public class SuperCakeMark
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        public Guid CreatedBy { get; set; }

        public bool IsDeleted { get; set; }

        public virtual ActiveDirectoryUser User { get; set; }
    }
}
