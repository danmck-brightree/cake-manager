using System.Threading.Tasks;
using CakeManager.Client.Models;
using Microsoft.AspNetCore.Components;

namespace CakeManager.Client.Components.Toast
{
    public class ToastComponent : ComponentBase
    {
        [Parameter] protected ToastMessage ToastMessage { get; set; }

        protected override async Task OnInitAsync()
        {
            await base.OnInitAsync();
        }
    }
}
