using CakeManager.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CakeManager.Client.Services.Interfaces
{
    public interface IUserService
    {
        bool? IsAdmin { get; set; }
        event Action onStatusChanged;
        Task<bool> IsCurrentUserAdmin();
        Task<List<ActiveDirectoryUser>> GetUsers();
        Task<bool> DeleteUser(string email);
        Task<bool> ToggleUserAdmin(string email);
    }
}
