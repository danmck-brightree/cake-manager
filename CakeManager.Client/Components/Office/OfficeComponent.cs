using System.Threading.Tasks;
using CakeManager.Client.Components.OfficeDropdown;
using CakeManager.Client.Components.TokenGuard;

namespace CakeManager.Client.Components.Office
{
    public class OfficeComponent : TokenGuardComponent
    {
        protected OfficeDropdownComponent OfficeDropdown { get; set; }

        protected async Task SaveOffice()
        {
            if (OfficeDropdown.SelectedOfficeId == default)
                return;

            await OfficeService.SaveCurrentUserOfficeId(OfficeDropdown.SelectedOfficeId);
        }
    }
}
