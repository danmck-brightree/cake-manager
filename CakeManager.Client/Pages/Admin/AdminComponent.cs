using System.Collections.Generic;
using System.Threading.Tasks;
using CakeManager.Client.Components.Error;
using CakeManager.Client.Components.TokenGuard;
using CakeManager.Client.Services.Interfaces;
using CakeManager.Shared;
using Microsoft.AspNetCore.Components;

namespace CakeManager.Client.Pages.Admin
{
    public class AdminComponent : TokenGuardComponent
    {
        [Inject] protected IUserService UserService { get; set; }

        protected ErrorComponent Error { get; set; }

        protected List<ActiveDirectoryUser> Users { get; set; } = new List<ActiveDirectoryUser>();

        private const string ToggleUserAdminFailedMessage = "Toggle admin failed.";
        private const string DeleteUserFailedMessage = "Delete user failed.";

        protected override async Task OnInitAsync()
        {
            if (!(await CheckIsAdmin()))
                return;

            Users = await UserService.GetUsers();

            await base.OnInitAsync();
        }

        protected async Task ClickAdmin(string email)
        {
            Error.ErrorMessage = null;

            if (!await UserService.ToggleUserAdmin(email))
                Error.ErrorMessage = ToggleUserAdminFailedMessage;
            else
            {
                if (!(await CheckIsAdmin()))
                    return;

                Users = await UserService.GetUsers();
            }
        }

        protected async Task ClickDelete(string email)
        {
            Error.ErrorMessage = null;

            if (!await UserService.DeleteUser(email))
                Error.ErrorMessage = DeleteUserFailedMessage;
            else
            {
                await CheckIsAdmin(false);

                var hasLocalUser = await TokenService.HasLocalUser();
                if (!hasLocalUser)
                {
                    UriHelper.NavigateTo(Constants.OfficeRoute);
                    return;
                }

                Users = await UserService.GetUsers();
            }
        }

        private async Task<bool> CheckIsAdmin(bool navigate = true)
        {
            var isAdmin = await UserService.IsCurrentUserAdmin();

            if (navigate && !isAdmin)
            {
                UriHelper.NavigateTo("/");
                return false;
            }

            return isAdmin;
        }
    }
}
