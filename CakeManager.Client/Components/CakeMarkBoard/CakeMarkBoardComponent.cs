using CakeManager.Client.Components.OfficeDropdown;
using CakeManager.Client.Services.Interfaces;
using CakeManager.Shared;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CakeManager.Client.Components.CakeMarkBoard
{
    public class CakeMarkBoardComponent : ComponentBase
    {
        [Inject] protected ICakeMarkService CakeMarkService { get; set; }
        [Inject] protected ITokenService TokenService { get; set; }

        protected OfficeDropdownComponent OfficeDropdown { get; set; }

        public CakeMarkGridData CakeMarkGridData { get; private set; } = new CakeMarkGridData();

        public bool GridLoading { get; set; }

        protected override async Task OnAfterRenderAsync()
        {
            Action<Guid> onSelectedOfficeChanged = async (Guid selectedOfficeId) => await ChangeOffice(selectedOfficeId);

            OfficeDropdown.onSelectedOfficeChanged -= onSelectedOfficeChanged;
            OfficeDropdown.onSelectedOfficeChanged += onSelectedOfficeChanged;

            await base.OnAfterRenderAsync();
        }

        protected async Task ChangeOffice(Guid selectedOfficeId)
        {
            this.GridLoading = true;
            StateHasChanged();

            this.CakeMarkGridData = await CakeMarkService.GetCakeMarkGridData(selectedOfficeId);
            this.GridLoading = false;

            StateHasChanged();
        }

        public async Task Refresh()
        {
            this.GridLoading = true;
            StateHasChanged();

            await this.ChangeOffice(this.OfficeDropdown.SelectedOfficeId);
            this.GridLoading = false;

            StateHasChanged();
        }
    }
}
