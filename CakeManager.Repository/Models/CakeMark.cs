using System;
using System.ComponentModel.DataAnnotations;

namespace CakeManager.Repository.Models
{
    public class CakeMark
    {
        [Key]
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public DateTime CreatedDate { get; set; }

        public Guid CreatedBy { get; set; }
    }
}
