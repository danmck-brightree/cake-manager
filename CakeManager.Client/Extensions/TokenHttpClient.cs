using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace CakeManager.Client.Extensions
{
    public class TokenHttpClient : ITokenHttpClient
    {
        private IJSRuntime JsRuntimeCurrent { get; set; }
        private HttpClient HttpClient { get; set; }

        public TokenHttpClient(IJSRuntime jsRuntimeCurrent, HttpClient httpClient)
        {
            this.JsRuntimeCurrent = jsRuntimeCurrent;
            this.HttpClient = httpClient;
        }

        public async Task<T> GetJsonAsync<T>(string url)
        {
            if (!await AddToken())
                return default(T);

            return await HttpClient.GetJsonAsync<T>(url);
        }

        public async Task<T> PostJsonAsync<T>(string url, object obj)
        {
            if (!await AddToken())
                return default(T);

            return await HttpClient.PostJsonAsync<T>(url, obj);
        }

        public async Task<bool> DeleteAsync(string url)
        {
            if (!await AddToken())
                return false;

            var result = await HttpClient.DeleteAsync(url);
            return result.IsSuccessStatusCode;
        }

        private async Task<bool> AddToken()
        {
            var token = await JsRuntimeCurrent.GetToken();

            if (token == null)
                return false;

            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            return true;
        }
    }
}
