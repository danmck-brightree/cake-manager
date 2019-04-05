using CakeManager.Logic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CakeManager.Server.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly IAccountLogic accountLogic;

        public AccountController(IAccountLogic accountLogic)
        {
            this.accountLogic = accountLogic;
        }

        [HttpGet("user")]
        public async Task<bool> HasLocalUser()
        {
            return await this.accountLogic.HasLocalUser();
        }
    }
}
