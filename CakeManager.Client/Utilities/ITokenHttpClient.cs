using System.Threading.Tasks;

namespace CakeManager.Client.Utilities
{
    public interface ITokenHttpClient
    {
        Task<T> GetJsonAsync<T>(string url);
        Task<T> PostJsonAsync<T>(string url, object obj);
        Task<bool> DeleteAsync(string url);
    }
}
