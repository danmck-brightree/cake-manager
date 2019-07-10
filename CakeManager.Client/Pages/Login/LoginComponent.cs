using CakeManager.Shared;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using CakeManager.Client.Services.Interfaces;
using CakeManager.Client.Components.Error;
using System;

namespace CakeManager.Client.Pages.Login
{
    public class LoginComponent : ComponentBase
    {
        [Inject] protected IUriHelper UriHelper { get; set; }
        [Inject] protected ITokenService TokenService { get; set; }

        protected override async Task OnInitAsync()
        {
            var token = await TokenService.GetToken();

            if (token != null)
                UriHelper.NavigateTo("/");

            await base.OnInitAsync();
        }

        public async Task Login()
        {
            await TokenService.LogIn();
        }
    }
}
