using CakeManager.Client.Extensions;
using CakeManager.Client.Services.Interfaces;
using CakeManager.Shared;
using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace CakeManager.Client.Services
{
    public class TokenService : ITokenService
    {
        public bool IsLoggedIn { get; set; }

        private IJSRuntime JsRuntimeCurrent { get; set; }

        public TokenService(IJSRuntime jsRuntimeCurrent)
        {
            this.JsRuntimeCurrent = jsRuntimeCurrent;
        }

        public async Task LogIn()
        {
            await JsRuntimeCurrent.LogIn();
        }

        public async Task LogOut()
        {
            await JsRuntimeCurrent.LogOut();
        }

        public async Task<User> GetUser()
        {
            return await JsRuntimeCurrent.GetUser();
        }

        public async Task<string> GetToken()
        {
            return await JsRuntimeCurrent.GetToken();
        }
    }
}
