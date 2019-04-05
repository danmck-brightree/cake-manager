using CakeManager.Client.Extensions;
using CakeManager.Client.Services.Interfaces;
using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace CakeManager.Client.Services
{
    public class ToastService : IToastService
    {
        private readonly IJSRuntime jSRuntime;

        public string Title { get; private set; }
        public string Message { get; private set; }

        public event Action onShowToast;

        public ToastService(IJSRuntime jSRuntime)
        {
            this.jSRuntime = jSRuntime;
        }

        public async Task ShowToast(string message, string title = "Success")
        {
            this.Title = title;
            this.Message = message;

            await this.jSRuntime.ShowToast();

            onShowToast?.Invoke();
        }
    }
}
