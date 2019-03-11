using Microsoft.AspNetCore.Components;

namespace CakeManager.Client.Components.Error
{
    public class ErrorComponent : ComponentBase
    {
        private string errorMessage;
        public string ErrorMessage
        {
            get
            {
                return errorMessage;
            }
            set
            {
                this.errorMessage = value;
                StateHasChanged();
            }
        }
    }
}
