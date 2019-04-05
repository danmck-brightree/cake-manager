﻿using CakeManager.Client.Components.OfficeDropdown;
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

        protected List<CakeMarkGridData> CakeMarkGridData { get; set; } = new List<CakeMarkGridData>();

        protected override async Task OnAfterRenderAsync()
        {
            OfficeDropdown.onSelectedOfficeChanged += async (Guid selectedOfficeId) => await ChangeOffice(selectedOfficeId);

            await base.OnAfterRenderAsync();
        }

        protected async Task ChangeOffice(Guid selectedOfficeId)
        {
            this.CakeMarkGridData = await CakeMarkService.GetCakeMarkGridData(selectedOfficeId);
        }

        public async Task Refresh()
        {
            await this.ChangeOffice(this.OfficeDropdown.SelectedOfficeId);

            StateHasChanged();
        }
    }
}
