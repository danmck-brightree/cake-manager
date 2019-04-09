using Microsoft.AspNetCore.Components;
using System;

namespace CakeManager.Client.Components.Modal
{
    public class ModalComponent : ComponentBase
    {
        [Parameter] protected string Id { get; set; } = "modal";
        [Parameter] protected string Title { get; set; } = "title";
        [Parameter] protected string ButtonText { get; set; } = "Continue";
        [Parameter] protected RenderFragment ModalContent { get; set; }

        protected string LabelId
        {
            get
            {
                return Id != null ? Id + "Label" : "modalLabel";
            }
        }

        public event Action onClick;

        protected void ClickButton()
        {
            onClick?.Invoke();
        }
    }
}
