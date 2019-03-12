using System.Threading.Tasks;
using CakeManager.Client.Components.CakeMarkBoard;
using CakeManager.Client.Components.CakeMarkTally;
using CakeManager.Client.Components.Error;
using CakeManager.Client.Services.Interfaces;
using CakeManager.Shared;
using Microsoft.AspNetCore.Components;

namespace CakeManager.Client.Components.CakeMark
{
    public class CakeMarkComponent : ComponentBase
    {
        [Inject] protected ICakeMarkService CakeMarkService { get; set; }
        [Inject] protected ITokenService TokenService { get; set; }

        protected ErrorComponent Error { get; set; }
        protected CakeMarkTallyComponent CakeMarkTally { get; set; }
        protected CakeMarkBoardComponent CakeMarkBoard { get; set; }

        private const string AddCakeMarkFailedMessage = "Add cake mark failed.";
        private const string RemoveCakeMarkFailedMessage = "Remove cake mark failed.";

        protected Shared.CakeMark CakeMark { get; set; } = new Shared.CakeMark();

        protected override async Task OnInitAsync()
        {
            await base.OnInitAsync();
        }

        protected async Task AddCakeMark()
        {
            CakeMark.UserId = Constants.TemporaryUserId;

            var result = await this.CakeMarkService.AddCakeMark(CakeMark);

            if (!result)
                Error.ErrorMessage = AddCakeMarkFailedMessage;
            else
                CakeMarkTally.CakeMarkTally++;

            await CakeMarkBoard.Refresh();
        }

        protected async Task RemoveCakeMark()
        {
            if (CakeMarkTally.CakeMarkTally == 0)
                return;

            CakeMark.UserId = Constants.TemporaryUserId;

            var result = await this.CakeMarkService.RemoveCakeMark(CakeMark);

            if (!result)
                Error.ErrorMessage = RemoveCakeMarkFailedMessage;
            else
                CakeMarkTally.CakeMarkTally--;

            await CakeMarkBoard.Refresh();
        }
    }
}
