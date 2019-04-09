using CakeManager.Client.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CakeManager.Client.Components.OfficeDropdown
{
    public class OfficeDropdownComponent : ComponentBase
    {
        [Inject] protected IOfficeService OfficeService { get; set; }

        public Guid SelectedOfficeId { get; set; }
        public event Action<Guid> onSelectedOfficeChanged;

        protected List<Shared.Office> Offices { get; set; } = new List<Shared.Office>();

        protected override async Task OnInitAsync()
        {
            this.Offices = await OfficeService.GetOffices();

            if (this.Offices == null)
                return;

            this.SelectedOfficeId = this.Offices
                .Where(x => x.Selected)
                .Select(x => x.Id.Value)
                .FirstOrDefault();

            if (SelectedOfficeId == default)
            {
                this.SelectedOfficeId = this.Offices
                    .Select(x => x.Id.Value)
                    .FirstOrDefault();
            }

            this.ChangeOffice(new UIChangeEventArgs
            {
                Value = SelectedOfficeId
            });

            await base.OnInitAsync();
        }

        protected void ChangeOffice(UIChangeEventArgs changeEvent)
        {
            this.SelectedOfficeId = new Guid(changeEvent.Value.ToString());
            this.onSelectedOfficeChanged?.Invoke(this.SelectedOfficeId);
        }
    }
}
