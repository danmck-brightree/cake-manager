using CakeManager.Logic;
using CakeManager.Shared;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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

        [HttpGet("supercakemark")]
        public async Task<int> GetSuperCakeMarkTally()
        {
            return await this.cakeMarkLogic.GetSuperCakeMarkTally();
        }

        [HttpPost("cakemark")]
        public async Task<bool> AddCakeMark()
        {
            return await this.cakeMarkLogic.AddCakeMark();
        }

        [HttpDelete("cakemark")]
        public async Task<IActionResult> RemoveCakeMark()
        {
            var success = await this.cakeMarkLogic.RemoveCakeMark();

            if (success)
                return Ok();
            else
                return BadRequest();
        }

        [HttpDelete("supercakemark")]
        public async Task<IActionResult> RemoveSuperCakeMark()
        {
            var success = await this.cakeMarkLogic.RemoveSuperCakeMark();

            if (success)
                return Ok();
            else
                return BadRequest();
        }

        [HttpGet("cakemarks")]
        public async Task<List<CakeMarkGridData>> GetCakeMarkGridData(Guid officeId)
        {
            return await this.cakeMarkLogic.GetCakeMarkGridData(officeId);
        }
    }
}
