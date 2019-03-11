using CakeManager.Client.Extensions;
using CakeManager.Client.Services.Interfaces;
using CakeManager.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace CakeManager.Client.Services
{
    public class TokenService : ITokenService
    {
        #region dependency injection

        private IJSRuntime JsRuntimeCurrent { get; set; }
        private HttpClient HttpClient { get; set; }

        #endregion

        public event Action onTokenChange;

        public bool IsLoggedIn { get; private set; }

        public TokenService(IJSRuntime jsRuntimeCurrent, HttpClient httpClient)
        {
            this.JsRuntimeCurrent = jsRuntimeCurrent;
            this.HttpClient = httpClient;
        }

        private const string LoginUrl = "/api/Account/Login";

        public async Task<bool> CheckToken()
        {
            var token = await JsRuntimeCurrent.GetItem(JSRuntimeExtensions.TokenKey);

            this.IsLoggedIn = !string.IsNullOrEmpty(token);

            onTokenChange?.Invoke();

            return this.IsLoggedIn;
        }

        public async Task<TokenResponse> LogIn(User user)
        {
            var result = await HttpClient.PostJsonAsync<TokenResponse>(LoginUrl, user);

            if (result.Success)
            {
                await JsRuntimeCurrent.SetItem(JSRuntimeExtensions.TokenKey, result.Token);
                this.IsLoggedIn = true;
                onTokenChange?.Invoke();
            }
            else
                await LogOut();

            return result;
        }

        public async Task LogOut()
        {
            await JsRuntimeCurrent.RemoveItem(JSRuntimeExtensions.TokenKey);

            this.IsLoggedIn = false;

            onTokenChange?.Invoke();
        }
    }
}
