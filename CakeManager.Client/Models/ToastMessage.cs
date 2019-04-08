using System;

namespace CakeManager.Client.Models
{
    public class ToastMessage
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public DateTime CreatedDate { get; } = DateTime.Now;
    }
}
