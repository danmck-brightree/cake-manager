using CakeManager.Client.Utilities;
using CakeManager.Client.Services.Interfaces;
using CakeManager.Shared;
using System;
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
        private const string CurrentUserOfficeUrl = "/api/Office/User";
        private const string DeleteOfficeUrl = "/api/Office/DeleteOffice";
        private const string EditOfficeUrl = "/api/Office/EditOffice";

        public async Task<List<Office>> GetOffices()
        {
            return await HttpClient.GetJsonAsync<List<Office>>(OfficeUrl);
        }

        public async Task<Guid> GetCurrentUserOfficeId()
        {
            return await HttpClient.GetJsonAsync<Guid>(CurrentUserOfficeUrl);
        }

        public async Task<bool> SaveCurrentUserOfficeId(Guid selectedOfficeId)
        {
            return await HttpClient.PostJsonAsync<bool>(CurrentUserOfficeUrl, selectedOfficeId);
        }

        public async Task<bool> EditOffice(Office office)
        {
            return await HttpClient.PostJsonAsync<bool>(EditOfficeUrl, office);
        }

        public async Task<bool> DeleteOffice(Guid officeId)
        {
            return await HttpClient.PostJsonAsync<bool>(DeleteOfficeUrl, officeId);
        }
    }
}
