using CakeManager.Client.Utilities;
using CakeManager.Client.Services.Interfaces;
using CakeManager.Shared;
using System;
using System.Threading.Tasks;

namespace CakeManager.Client.Services
{
    public class CakeMarkService : ICakeMarkService
    {
        private ITokenHttpClient HttpClient { get; set; }

        public CakeMarkService(ITokenHttpClient httpClient)
        {
            this.HttpClient = httpClient;
        }

        private const string CakeMarkUrl = "/api/CakeMark/CakeMark";
        private const string CakeMarksUrl = "/api/CakeMark/CakeMarks";
        private const string SuperCakeMarkUrl = "/api/CakeMark/SuperCakeMark";
        private const string CakeMarkDeleteUrl = "/api/CakeMark/DeleteCakeMark";
        private const string SuperCakeMarkDeleteUrl = "/api/CakeMark/DeleteSuperCakeMark";

        public async Task<int> GetCakeMarkTally()
        {
            var result = await HttpClient.GetJsonAsync<int>(CakeMarkUrl);

            return result;
        }

        public async Task<int> GetSuperCakeMarkTally()
        {
            var result = await HttpClient.GetJsonAsync<int>(SuperCakeMarkUrl);

            return result;
        }

        public async Task<CakeMarkResult> AddCakeMark(DateTime latestEventDate)
        {
            var result = await HttpClient.PostJsonAsync<CakeMarkResult>(CakeMarkUrl, latestEventDate);

            return result;
        }

        public async Task<CakeMarkResult> RemoveCakeMark(DateTime latestEventDate)
        {
            var result = await HttpClient.PostJsonAsync<CakeMarkResult>(CakeMarkDeleteUrl, latestEventDate);

            return result;
        }

        public async Task<CakeMarkResult> RemoveSuperCakeMark(DateTime latestEventDate)
        {
            var result = await HttpClient.PostJsonAsync<CakeMarkResult>(SuperCakeMarkDeleteUrl, latestEventDate);

            return result;
        }

        public async Task<CakeMarkGridData> GetCakeMarkGridData(Guid selectedOfficeId)
        {
            var result = await HttpClient.GetJsonAsync<CakeMarkGridData>(CakeMarksUrl + string.Format("?officeId={0}", selectedOfficeId));

            return result;
        }
    }
}
