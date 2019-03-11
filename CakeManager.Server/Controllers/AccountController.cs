using CakeManager.Shared;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CakeManager.Server.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        [HttpPost("[action]")]
        public TokenResponse Login([FromBody]User user)
        {
            var token = Guid.NewGuid().ToString();
            var success = true;

            return new TokenResponse
            {
                Success = success,
                Token = token
            };
        }
    }
}
