using System.Threading.Tasks;
using CakeManager.Client.Components.Error;
using CakeManager.Client.Components.OfficeDropdown;
using CakeManager.Client.Components.TokenGuard;
using CakeManager.Client.Services.Interfaces;
using CakeManager.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Services;

namespace CakeManager.Client.Pages.Office
{
    public class OfficeComponent : TokenGuardComponent
    {
        [Inject] protected IOfficeService OfficeService { get; set; }

        protected OfficeDropdownComponent OfficeDropdown { get; set; }
        protected ErrorComponent Error { get; set; }

        private const string SaveOfficeFailedMessage = "Save office failed.";

        protected async Task SaveOffice()
        {
            Error.ErrorMessage = null;

            if (OfficeDropdown.SelectedOfficeId == default)
                return;

            if (await OfficeService.SaveCurrentUserOfficeId(OfficeDropdown.SelectedOfficeId))
                UriHelper.NavigateTo(Constants.CakeMarkRoute);
            else
                Error.ErrorMessage = SaveOfficeFailedMessage;
        }
    }
}
