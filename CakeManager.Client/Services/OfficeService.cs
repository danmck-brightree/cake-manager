using CakeManager.Client.Extensions;
using CakeManager.Client.Services.Interfaces;
using CakeManager.Shared;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CakeManager.Client.Services
{
    public class OfficeService : IOfficeService
    {
        private ITokenHttpClient HttpClient { get; set; }

        public OfficeService(ITokenHttpClient httpClient)
        {
            this.HttpClient = httpClient;
        }

        private const string OfficeUrl = "/api/Office/Offices";

        public async Task<List<Office>> GetOffices()
        {
            return await HttpClient.GetJsonAsync<List<Office>>(OfficeUrl);
        }
    }
}
