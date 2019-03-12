using CakeManager.Client.Services.Interfaces;
using CakeManager.Shared;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace CakeManager.Client.Services
{
    public class CakeMarkService : ICakeMarkService
    {
        private HttpClient HttpClient { get; set; }

        public CakeMarkService(HttpClient httpClient)
        {
            this.HttpClient = httpClient;
        }

        private const string CakeMarkUrl = "/api/CakeMark/CakeMark";
        private const string CakeMarksUrl = "/api/CakeMark/CakeMarks";

        public async Task<int> GetCakeMarkTally()
        {
            var result = await HttpClient.GetJsonAsync<int>(CakeMarkUrl);

            return result;
        }

        public async Task<bool> AddCakeMark(CakeMark cakeMark)
        {
            var result = await HttpClient.PostJsonAsync<bool>(CakeMarkUrl, cakeMark);

            return result;
        }

        public async Task<bool> RemoveCakeMark(CakeMark cakeMark)
        {
            var result = await HttpClient.DeleteAsync(CakeMarkUrl + "?userId=" + cakeMark.UserId);

            return result.IsSuccessStatusCode;
        }

        public async Task<List<CakeMarkGridData>> GetCakeMarkGridData(Guid selectedOfficeId)
        {
            var result = await HttpClient.GetJsonAsync<List<CakeMarkGridData>>(CakeMarksUrl + string.Format("?officeId={0}", selectedOfficeId));

            return result;
        }
    }
}
