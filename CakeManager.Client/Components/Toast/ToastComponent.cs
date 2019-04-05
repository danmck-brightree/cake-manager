using System.Threading.Tasks;
using CakeManager.Client.Services.Interfaces;
using Microsoft.AspNetCore.Components;

namespace CakeManager.Client.Components.Toast
{
    public class ToastComponent : ComponentBase
    {
        [Inject] protected IToastService ToastService { get; set; }

        protected override async Task OnInitAsync()
        {
            ToastService.onShowToast += () => StateHasChanged();

            await base.OnInitAsync();
        }
    }
}
