using CakeManager.Shared;
using Microsoft.AspNetCore.Mvc;

namespace CakeManager.Server.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        [HttpPost("[action]")]
        public TokenResponse Login(User user)
        {
            return new TokenResponse
            {
                Success = true,
                Token = "abc123"
            };
        }
    }
}
