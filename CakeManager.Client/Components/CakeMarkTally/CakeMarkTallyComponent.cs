using System.Threading.Tasks;
using CakeManager.Client.Services.Interfaces;
using Microsoft.AspNetCore.Components;

namespace CakeManager.Client.Components.CakeMarkTally
{
    public class CakeMarkTallyComponent : ComponentBase
    {
        [Inject] protected ICakeMarkService CakeMarkService { get; set; }

        private int? cakeMarkTally;
        public int? CakeMarkTally
        {
            get
            {
                return cakeMarkTally;
            }
            set
            {
                this.cakeMarkTally = value;
                StateHasChanged();
            }
        }

        protected override async Task OnInitAsync()
        {
            this.CakeMarkTally = await this.CakeMarkService.GetCakeMarkTally();
            await base.OnInitAsync();
        }
    }
}
