using CakeManager.Shared;
using System.Threading.Tasks;

namespace CakeManager.Client.Services.Interfaces
{
    public interface ITokenService
    {
        bool IsLoggedIn { get; set; }
        Task LogIn();
        Task LogOut();
        Task<User> GetUser();
        Task<string> GetToken();
    }
}
