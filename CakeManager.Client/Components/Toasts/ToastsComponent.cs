using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CakeManager.Client.Components.Toast;
using CakeManager.Client.Models;
using CakeManager.Client.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;

namespace CakeManager.Client.Components.Toasts
{
    public class ToastsComponent : ComponentBase
    {
        [Inject] protected IToastService ToastService { get; set; }

        protected List<ToastMessage> Toasts { get; private set; } = new List<ToastMessage>();

        private Dictionary<Guid, int> renderedIds = new Dictionary<Guid, int>();

        protected override async Task OnInitAsync()
        {
            ToastService.onShowToast += () => AddToast();

            await base.OnInitAsync();
        }

        private void AddToast()
        {
            var toastMessage = new ToastMessage
            {
                Id = ToastService.Id,
                Title = ToastService.Title,
                Message = ToastService.Message,
            };

            Toasts.Add(toastMessage);

            Toasts = Toasts
                .OrderByDescending(x => x.CreatedDate)
                .Take(3)
                .ToList();

            StateHasChanged();
        }
    }
}
