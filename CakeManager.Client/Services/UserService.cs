using CakeManager.Client.Utilities;
using CakeManager.Client.Services.Interfaces;
using CakeManager.Shared;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Net.Http;

namespace CakeManager.Client.Services
{
    public class UserService : IUserService
    {
        private ITokenHttpClient HttpClient { get; set; }

        public bool? IsAdmin { get; set; }

        public event Action onStatusChanged;

        public UserService(ITokenHttpClient httpClient)
        {
            this.HttpClient = httpClient;
        }

        private const string AdminUrl = "/api/Account/Admin";
        private const string UsersUrl = "/api/Account/Users";
        private const string DeleteUserUrl = "/api/Account/DeleteUser";

        public async Task<bool> IsCurrentUserAdmin()
        {
            try
            {
                var result = await HttpClient.GetJsonAsync<bool>(AdminUrl);

                IsAdmin = result;

                onStatusChanged?.Invoke();

                return result;
            }
            catch (HttpRequestException)
            {
                IsAdmin = false;

                onStatusChanged?.Invoke();

                return false;
            }
        }
        public async Task<List<ActiveDirectoryUser>> GetUsers()
        {
            return await HttpClient.GetJsonAsync<List<ActiveDirectoryUser>>(UsersUrl);
        }

        public async Task<bool> DeleteUser(string email)
        {
            return await HttpClient.PostJsonAsync<bool>(DeleteUserUrl, email);
        }

        public async Task<bool> ToggleUserAdmin(string email)
        {
            return await HttpClient.PostJsonAsync<bool>(AdminUrl, email);
        }
    }
}
