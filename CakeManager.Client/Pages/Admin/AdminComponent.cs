using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CakeManager.Client.Components.Error;
using CakeManager.Client.Components.Modal;
using CakeManager.Client.Components.TokenGuard;
using CakeManager.Client.Services.Interfaces;
using CakeManager.Client.Utilities;
using CakeManager.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace CakeManager.Client.Pages.Admin
{
    public class AdminComponent : TokenGuardComponent
    {
        [Inject] protected IUserService UserService { get; set; }
        [Inject] protected IOfficeService OfficeService { get; set; }
        [Inject] protected IJSRuntime JSRuntime { get; set; }

        protected ErrorComponent Error { get; set; }
        protected ModalComponent EditOfficeModal { get; set; }
        protected ModalComponent DeleteOfficeModal { get; set; }

        protected Shared.Office SelectedOffice { get; set; }

        protected List<ActiveDirectoryUser> Users { get; set; } = new List<ActiveDirectoryUser>();
        protected List<Shared.Office> Offices { get; set; } = new List<Shared.Office>();

        private const string ToggleUserAdminFailedMessage = "Toggle admin failed.";
        private const string DeleteUserFailedMessage = "Delete user failed.";
        private const string DeleteOfficeErrorMessage = "Delete office failed.";
        private const string SaveOfficeErrorMessage = "Save office failed.";

        protected override async Task OnInitAsync()
        {
            if (!(await CheckIsAdmin()))
                return;

            Users = await UserService.GetUsers();
            Offices = await OfficeService.GetOffices();

            await base.OnInitAsync();
        }

        protected override async Task OnAfterRenderAsync()
        {
            Action editOfficeModalClick = async () => await SaveOffice();

            EditOfficeModal.onClick -= editOfficeModalClick;
            EditOfficeModal.onClick += editOfficeModalClick;

            Action deleteOfficeModalClick = async () => await DeleteOffice();

            DeleteOfficeModal.onClick -= deleteOfficeModalClick;
            DeleteOfficeModal.onClick += deleteOfficeModalClick;

            await base.OnAfterRenderAsync();
        }

        protected async Task ClickUserAdmin(string email)
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

        protected async Task ClickUserDelete(string email)
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

        protected async Task ClickOfficeAdd()
        {
            this.SelectedOffice = new Shared.Office();
            await JSRuntime.ShowModal("editOfficeModal");
        }

        protected async Task ClickOfficeEdit(Shared.Office office)
        {
            this.SelectedOffice = office;
            await JSRuntime.ShowModal("editOfficeModal");
        }

        protected async Task ClickOfficeDelete(Shared.Office office)
        {
            this.SelectedOffice = office;
            await JSRuntime.ShowModal("deleteOfficeModal");
        }

        private async Task SaveOffice()
        {
            this.Error.ErrorMessage = null;

            if (!(await OfficeService.EditOffice(this.SelectedOffice)))
                this.Error.ErrorMessage = SaveOfficeErrorMessage;
            else
                Offices = await OfficeService.GetOffices();

            StateHasChanged();

            await JSRuntime.HideModal("editOfficeModal");
        }

        private async Task DeleteOffice()
        {
            this.Error.ErrorMessage = null;

            if (!(await OfficeService.DeleteOffice(this.SelectedOffice.Id.Value)))
                this.Error.ErrorMessage = DeleteOfficeErrorMessage;
            else
                Offices = await OfficeService.GetOffices();

            await JSRuntime.HideModal("deleteOfficeModal");

            StateHasChanged();
        }
    }
}
