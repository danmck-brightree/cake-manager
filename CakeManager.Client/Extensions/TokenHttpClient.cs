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
            await AddToken();
            return await HttpClient.GetJsonAsync<T>(url);
        }

        public async Task<T> PostJsonAsync<T>(string url, object obj)
        {
            await AddToken();
            return await HttpClient.PostJsonAsync<T>(url, obj);
        }

        public async Task<bool> DeleteAsync(string url)
        {
            await AddToken();
            var result = await HttpClient.DeleteAsync(url);
            return result.IsSuccessStatusCode;
        }

        private async Task AddToken()
        {
            var token = await JsRuntimeCurrent.GetToken();
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }
}
