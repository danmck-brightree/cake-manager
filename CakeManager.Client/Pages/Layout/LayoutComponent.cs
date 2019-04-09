using CakeManager.Client.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Layouts;
using System.Threading.Tasks;

namespace CakeManager.Client.Pages.Layout
{
    public class LayoutComponent : LayoutComponentBase
    {
        [Inject] protected ITokenService TokenService { get; set; }
        [Inject] protected IUserService UserService { get; set; }

        protected bool CollapseNavMenu { get; set; } = true;

        protected string NavMenuCssClass => CollapseNavMenu ? "collapse" : null;
        
        protected override async Task OnInitAsync()
        {
            TokenService.onStatusChanged += StateHasChanged;

            await TokenService.GetToken();

            UserService.onStatusChanged += StateHasChanged;

            await UserService.IsCurrentUserAdmin();

            await base.OnInitAsync();
        }

        protected async Task LogOut()
        {
            await TokenService.LogOut();
        }

        protected void ToggleNavMenu()
        {
            CollapseNavMenu = !CollapseNavMenu;
        }
    }
}
