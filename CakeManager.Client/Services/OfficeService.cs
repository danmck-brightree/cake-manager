using CakeManager.Client.Services.Interfaces;
using CakeManager.Shared;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace CakeManager.Client.Services
{
    public class OfficeService : IOfficeService
    {
        private HttpClient HttpClient { get; set; }

        public OfficeService(HttpClient httpClient)
        {
            this.HttpClient = httpClient;
        }

        private const string OfficeUrl = "/api/Office/Offices";

        public async Task<List<Office>> GetOffices()
        {
            var result = await HttpClient.GetJsonAsync<List<Office>>(OfficeUrl);

            return result;
        }
    }
}
