using CakeManager.Shared;
using System;
using System.Threading.Tasks;

namespace CakeManager.Client.Services.Interfaces
{
    public interface ITokenService
    {
        bool IsLoggedIn { get; set; }
        bool IsRegistered { get; set; }
        event Action onStatusChanged;
        Task LogIn();
        Task LogOut();
        Task<User> GetUser();
        Task<string> GetToken();
        Task<bool> HasLocalUser();
    }
}
