using CakeManager.Client.Components.TokenGuard;
using CakeManager.Client.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Layouts;
using System.Threading.Tasks;

namespace CakeManager.Client.Components.Layout
{
    public class LayoutComponent : LayoutComponentBase
    {
        [Inject] protected ITokenService TokenService { get; set; }

        protected TokenGuardComponent TokenGuard { get; set; }

        protected bool CollapseNavMenu { get; set; } = true;

        protected string NavMenuCssClass => CollapseNavMenu ? "collapse" : null;

        protected override async Task OnInitAsync()
        {
            TokenService.onTokenChange += StateHasChanged;
            await base.OnInitAsync();
        }

        protected async Task LogOut()
        {
            await TokenService.LogOut();
            await TokenGuard.CheckToken();
        }

        protected void ToggleNavMenu()
        {
            CollapseNavMenu = !CollapseNavMenu;
        }
    }
}
