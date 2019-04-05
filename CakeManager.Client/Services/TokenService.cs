using CakeManager.Client.Extensions;
using CakeManager.Client.Services.Interfaces;
using CakeManager.Shared;
using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace CakeManager.Client.Services
{
    public class TokenService : ITokenService
    {
        private const string HasLocalUserUrl = "/api/Account/User";

        public bool IsLoggedIn { get; set; }

        public bool IsRegistered { get; set; }

        private IJSRuntime JsRuntimeCurrent { get; set; }

        private ITokenHttpClient HttpClient { get; set; }

        public event Action onStatusChanged;

        public TokenService(IJSRuntime jsRuntimeCurrent, ITokenHttpClient httpClient)
        {
            this.JsRuntimeCurrent = jsRuntimeCurrent;
            this.HttpClient = httpClient;
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
            var token = await JsRuntimeCurrent.GetToken();

            this.IsLoggedIn = token != null;

            onStatusChanged?.Invoke();

            return token;
        }

        public async Task<bool> HasLocalUser()
        {
            if (this.IsRegistered)
                return true;

            var hasLocalUser = await HttpClient.GetJsonAsync<bool>(HasLocalUserUrl);

            this.IsRegistered = hasLocalUser;

            onStatusChanged?.Invoke();

            return hasLocalUser;
        }
    }
}
