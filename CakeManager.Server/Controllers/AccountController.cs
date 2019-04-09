using CakeManager.Logic;
using CakeManager.Server.Authorization;
using CakeManager.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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

        [HttpGet("admin")]
        [Authorize(Policy = Policies.IsAdmin)]
        public bool IsCurrentUserAdmin()
        {
            return true;
        }

        [HttpGet("users")]
        [Authorize(Policy = Policies.IsAdmin)]
        public async Task<List<ActiveDirectoryUser>> GetUsers()
        {
            return await this.accountLogic.GetUsers();
        }

        [HttpPost("deleteuser")]
        [Authorize(Policy = Policies.IsAdmin)]
        public async Task<bool> DeleteUser([FromBody]string email)
        {
            return await this.accountLogic.DeleteUser(email);
        }

        [HttpPost("admin")]
        [Authorize(Policy = Policies.IsAdmin)]
        public async Task<bool> ToggleUserAdmin([FromBody]string email)
        {
            return await this.accountLogic.ToggleUserAdmin(email);
        }
    }
}
