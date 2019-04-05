using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CakeManager.Server.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class PingController : Controller
    {
        [HttpGet]
        public string Ping()
        {
            return "Pong";
        }
    }
}
