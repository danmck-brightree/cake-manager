using System;
using System.Threading.Tasks;
using CakeManager.Client.Components.CakeMarkBoard;
using CakeManager.Client.Components.CakeMarkTally;
using CakeManager.Client.Components.Error;
using CakeManager.Client.Components.Modal;
using CakeManager.Client.Components.TokenGuard;
using CakeManager.Client.Utilities;
using CakeManager.Client.Services.Interfaces;
using CakeManager.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace CakeManager.Client.Pages.CakeMark
{
    public class CakeMarkComponent : TokenGuardComponent
    {
        [Inject] protected ICakeMarkService CakeMarkService { get; set; }
        [Inject] protected IJSRuntime JSRuntime { get; set; }
        [Inject] protected IToastService ToastService { get; set; }

        protected ErrorComponent Error { get; set; }
        protected CakeMarkTallyComponent CakeMarkTally { get; set; }
        protected CakeMarkTallyComponent SuperCakeMarkTally { get; set; }
        protected CakeMarkBoardComponent CakeMarkBoard { get; set; }
        protected ModalComponent Modal { get; set; }

        private const string AddCakeMarkFailedMessage = "Add cake mark failed.";
        private const string RemoveCakeMarkFailedMessage = "Remove cake mark failed.";
        private const string AddCakeMarkSuccessMessage = "Add cake mark success.";
        private const string RemoveCakeMarkSuccessMessage = "Remove cake mark success.";
        private const string RemoveSuperCakeMarkSuccessMessage = "Remove super cake mark success.";

        protected bool CanRemoveCakeMark
        {
            get
            {
                return !(CakeMarkTally != null && CakeMarkTally.CakeMarkTally.HasValue && CakeMarkTally.CakeMarkTally.Value > 0);
            }
        }

        protected bool CanRemoveSuperCakeMark
        {
            get
            {
                return !(SuperCakeMarkTally != null && SuperCakeMarkTally.CakeMarkTally.HasValue && SuperCakeMarkTally.CakeMarkTally.Value > 0);
            }
        }

        protected override async Task OnAfterRenderAsync()
        {
            Action onClickAction = async () => await AddConfirmedCakeMark();

            Modal.onClick -= onClickAction;
            Modal.onClick += onClickAction;

            CakeMarkTally.OnCakeMarkTallyUpdate -= StateHasChanged;
            CakeMarkTally.OnCakeMarkTallyUpdate += StateHasChanged;
            SuperCakeMarkTally.OnCakeMarkTallyUpdate -= StateHasChanged;
            SuperCakeMarkTally.OnCakeMarkTallyUpdate += StateHasChanged;

            await base.OnAfterRenderAsync();
        }

        protected async Task AddCakeMark()
        {
            if ((CakeMarkTally.CakeMarkTally + 1) == Constants.CakeMarkTallyMax)
            {
                await JSRuntime.ShowModal("addCakeMarkModal");
                return;
            }

            await this.AddConfirmedCakeMark();
        }

        protected async Task AddConfirmedCakeMark()
        {
            Error.ErrorMessage = null;

            var result = await this.CakeMarkService.AddCakeMark();

            await JSRuntime.HideModal("addCakeMarkModal");

            if (!result)
                Error.ErrorMessage = AddCakeMarkFailedMessage;
            else
            {
                await ToastService.ShowToast(AddCakeMarkSuccessMessage);

                if ((CakeMarkTally.CakeMarkTally + 1) < Constants.CakeMarkTallyMax)
                    CakeMarkTally.CakeMarkTally++;
                else
                {
                    CakeMarkTally.CakeMarkTally = 0;
                    if (SuperCakeMarkTally.CakeMarkTally < Constants.SuperCakeMarkTallyMax)
                        SuperCakeMarkTally.CakeMarkTally++;
                }
            }

            await CakeMarkBoard.Refresh();
        }

        protected async Task RemoveCakeMark()
        {
            Error.ErrorMessage = null;

            if (CakeMarkTally.CakeMarkTally == 0)
                return;

            var result = await this.CakeMarkService.RemoveCakeMark();

            if (!result)
                Error.ErrorMessage = RemoveCakeMarkFailedMessage;
            else
            {
                await ToastService.ShowToast(RemoveCakeMarkSuccessMessage);
                CakeMarkTally.CakeMarkTally--;
            }

            await CakeMarkBoard.Refresh();
        }

        protected async Task RemoveSuperCakeMark()
        {
            Error.ErrorMessage = null;

            if (SuperCakeMarkTally.CakeMarkTally == 0)
                return;

            var result = await this.CakeMarkService.RemoveSuperCakeMark();

            if (!result)
                Error.ErrorMessage = RemoveCakeMarkFailedMessage;
            else
            {
                SuperCakeMarkTally.CakeMarkTally--;
                await ToastService.ShowToast(RemoveSuperCakeMarkSuccessMessage);
            }

            await CakeMarkBoard.Refresh();
        }
    }
}
