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
        public async Task<CakeMarkResult> AddCakeMark([FromBody]DateTime latestEventDate)
        {
            return await this.cakeMarkLogic.AddCakeMark(latestEventDate);
        }

        [HttpPost("deletecakemark")]
        public async Task<CakeMarkResult> RemoveCakeMark([FromBody]DateTime latestEventDate)
        {
            return await this.cakeMarkLogic.RemoveCakeMark(latestEventDate);
        }

        [HttpPost("deletesupercakemark")]
        public async Task<CakeMarkResult> RemoveSuperCakeMark([FromBody]DateTime latestEventDate)
        {
            return await this.cakeMarkLogic.RemoveSuperCakeMark(latestEventDate);
        }

        [HttpGet("cakemarks")]
        public async Task<CakeMarkGridData> GetCakeMarkGridData(Guid officeId)
        {
            return await this.cakeMarkLogic.GetCakeMarkGridData(officeId);
        }
    }
}
