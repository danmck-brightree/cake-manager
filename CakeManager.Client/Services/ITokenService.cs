using CakeManager.Shared;
using System;
using System.Threading.Tasks;

namespace CakeManager.Client.Services
{
    public interface ITokenService
    {
        event Action onTokenChange;
        bool IsLoggedIn { get; }
        Task<bool> CheckToken();
        Task<TokenResponse> LogIn(User user);
        Task LogOut();
    }
}
