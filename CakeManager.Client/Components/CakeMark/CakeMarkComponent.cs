using System.Threading.Tasks;
using CakeManager.Client.Components.CakeMarkBoard;
using CakeManager.Client.Components.CakeMarkTally;
using CakeManager.Client.Components.Error;
using CakeManager.Client.Components.TokenGuard;
using CakeManager.Client.Extensions;
using CakeManager.Client.Services.Interfaces;
using CakeManager.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace CakeManager.Client.Components.CakeMark
{
    public class CakeMarkComponent : TokenGuardComponent
    {
        [Inject] protected ICakeMarkService CakeMarkService { get; set; }
        [Inject] protected IJSRuntime JSRuntime { get; set; }

        protected ErrorComponent Error { get; set; }
        protected CakeMarkTallyComponent CakeMarkTally { get; set; }
        protected CakeMarkTallyComponent SuperCakeMarkTally { get; set; }
        protected CakeMarkBoardComponent CakeMarkBoard { get; set; }

        private const string AddCakeMarkFailedMessage = "Add cake mark failed.";
        private const string RemoveCakeMarkFailedMessage = "Remove cake mark failed.";

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
            var result = await this.CakeMarkService.AddCakeMark();

            await JSRuntime.HideModal("addCakeMarkModal");

            if (!result)
                Error.ErrorMessage = AddCakeMarkFailedMessage;
            else
            {
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
            if (CakeMarkTally.CakeMarkTally == 0)
                return;

            var result = await this.CakeMarkService.RemoveCakeMark();

            if (!result)
                Error.ErrorMessage = RemoveCakeMarkFailedMessage;
            else
                CakeMarkTally.CakeMarkTally--;

            await CakeMarkBoard.Refresh();
        }

        protected async Task RemoveSuperCakeMark()
        {
            if (SuperCakeMarkTally.CakeMarkTally == 0)
                return;

            var result = await this.CakeMarkService.RemoveSuperCakeMark();

            if (!result)
                Error.ErrorMessage = RemoveCakeMarkFailedMessage;
            else
                SuperCakeMarkTally.CakeMarkTally--;

            await CakeMarkBoard.Refresh();
        }
    }
}
