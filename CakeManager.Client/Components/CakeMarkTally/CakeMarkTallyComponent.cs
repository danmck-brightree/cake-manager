using System.Threading.Tasks;
using CakeManager.Client.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using static CakeManager.Shared.Constants;

namespace CakeManager.Client.Components.CakeMarkTally
{
    public class CakeMarkTallyComponent : ComponentBase
    {
        [Inject] protected ICakeMarkService CakeMarkService { get; set; }

        [Parameter] protected CakeMarkType CakeMarkType { get; set; } = CakeMarkType.Normal;

        protected string CakeMarkDisplay
        {
            get
            {
                var message = "Loading ...";

                if (CakeMarkTally.HasValue)
                {
                    switch (CakeMarkType)
                    {
                        case CakeMarkType.Normal:
                            message = string.Format("You have {0} cake marks.", CakeMarkTally.Value);
                            break;
                        case CakeMarkType.Super:
                            message = string.Format("You have {0} super cake marks.", CakeMarkTally.Value);
                            break;
                    }
                }

                return message;
            }
        }

        protected string CakeMarkClass
        {
            get
            {
                return CakeMarkType == CakeMarkType.Normal ? "alert-success" : "alert-warning";
            }
        }

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
            switch (this.CakeMarkType)
            {
                case CakeMarkType.Normal:
                    this.CakeMarkTally = await this.CakeMarkService.GetCakeMarkTally();
                    break;
                case CakeMarkType.Super:
                    this.CakeMarkTally = await this.CakeMarkService.GetSuperCakeMarkTally();
                    break;
            }
            await base.OnInitAsync();
        }
    }
}
