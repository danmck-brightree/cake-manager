using CakeManager.Shared;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Services;
using CakeManager.Client.Services;

namespace CakeManager.Client.Components.Login
{
    public class LoginComponent : ComponentBase
    {
        #region dependency injection

        [Inject]
        protected IUriHelper UriHelper { get; set; }

        [Inject]
        protected ITokenService TokenService { get; set; }

        #endregion

        private const string LoginFailedMessage = "Login failed.";

        protected string ErrorMessage { get; set; }
        protected User User { get; set; } = new User();

        public async Task Login()
        {
            var result = await TokenService.LogIn(User);

            if (result.Success)
                UriHelper.NavigateTo("/");
            else
                ErrorMessage = LoginFailedMessage;
        }
    }
}
