using CakeManager.Logic;
using CakeManager.Shared;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace CakeManager.Server.Controllers
{
    [Route("api/[controller]")]
    public class CakeMarkController : Controller
    {
        private readonly ICakeMarkLogic cakeMarkLogic;

        public CakeMarkController(ICakeMarkLogic cakeMarkLogic)
        {
            this.cakeMarkLogic = cakeMarkLogic;
        }

        [HttpGet("cakemark")]
        public async Task<int> GetCakeMarkTally()
        {
            return await this.cakeMarkLogic.GetCakeMarkTally();
        }

        [HttpPost("cakemark")]
        public async Task<bool> AddCakeMark([FromBody]CakeMark cakeMark)
        {
            return await this.cakeMarkLogic.AddCakeMark(cakeMark);
        }

        [HttpDelete("cakemark")]
        public async Task<IActionResult> RemoveCakeMark(Guid userId)
        {
            var success = await this.cakeMarkLogic.RemoveCakeMark(userId);

            if (success)
                return Ok();
            else
                return BadRequest();
        }
    }
}
