using System;
using System.Threading.Tasks;

namespace CakeManager.Client.Services.Interfaces
{
    public interface IToastService
    {
        string Title { get; }
        string Message { get; }
        event Action onShowToast;
        Task ShowToast(string message, string title = "Success");
    }
}
