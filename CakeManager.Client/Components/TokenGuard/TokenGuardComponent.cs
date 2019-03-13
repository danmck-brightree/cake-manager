using System.Threading.Tasks;
using CakeManager.Client.Services.Interfaces;
using CakeManager.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Services;

namespace CakeManager.Client.Components.TokenGuard
{
    public class TokenGuardComponent : ComponentBase
    {
        [Inject] protected IUriHelper UriHelper { get; set; }
        [Inject] protected ITokenService TokenService { get; set; }

        protected override async Task OnInitAsync()
        {
            var token = await TokenService.GetToken();

            if (token == null)
                UriHelper.NavigateTo(Constants.LoginRoute);

            await base.OnInitAsync();
        }
    }
}
