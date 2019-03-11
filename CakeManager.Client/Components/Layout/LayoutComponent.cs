using CakeManager.Client.Components.TokenGuard;
using CakeManager.Client.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Layouts;
using System.Threading.Tasks;

namespace CakeManager.Client.Components.Layout
{
    public class LayoutComponent : LayoutComponentBase
    {
        #region dependency injection

        [Inject]
        protected ITokenService TokenService { get; set; }

        #endregion

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
