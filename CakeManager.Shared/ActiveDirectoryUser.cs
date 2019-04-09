using System;

namespace CakeManager.Shared
{
    public class ActiveDirectoryUser
    {
        public Guid Id { get; set; }

        public string Email { get; set; }

        public Guid OfficeId { get; set; }

        public string Office { get; set; }

        public bool IsAdmin { get; set; }
    }
}
