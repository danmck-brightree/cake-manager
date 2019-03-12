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
        [Inject] protected IOfficeService OfficeService { get; set; }
        [Inject] protected ICakeMarkService CakeMarkService { get; set; }

        protected List<Office> Offices { get; set; } = new List<Office>();
        protected List<CakeMarkGridData> CakeMarkGridData { get; set; } = new List<CakeMarkGridData>();

        protected Guid SelectedOfficeId { get; set; }

        protected override async Task OnInitAsync()
        {
            this.Offices = await OfficeService.GetOffices();

            this.SelectedOfficeId = this.Offices
                .Where(x => x.Selected)
                .Select(x => x.Id)
                .FirstOrDefault();

            await this.ChangeOffice(new UIChangeEventArgs
            {
                Value = SelectedOfficeId
            });

            StateHasChanged();

            await base.OnInitAsync();
        }

        protected async Task ChangeOffice(UIChangeEventArgs changeEvent)
        {
            this.SelectedOfficeId = new Guid(changeEvent.Value.ToString());
            this.CakeMarkGridData = await CakeMarkService.GetCakeMarkGridData(this.SelectedOfficeId);
        }

        public async Task Refresh()
        {
            await this.ChangeOffice(new UIChangeEventArgs
            {
                Value = this.SelectedOfficeId
            });

            StateHasChanged();
        }
    }
}
