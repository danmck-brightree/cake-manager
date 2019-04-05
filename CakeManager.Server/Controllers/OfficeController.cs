using CakeManager.Logic;
using CakeManager.Shared;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CakeManager.Server.Controllers
{
    [Route("api/[controller]")]
    public class OfficeController : Controller
    {
        private readonly IOfficeLogic officeLogic;

        public OfficeController(IOfficeLogic officeLogic)
        {
            this.officeLogic = officeLogic;
        }

        [HttpGet("offices")]
        public async Task<List<Office>> GetOffices()
        {
            return await this.officeLogic.GetOffices();
        }

        [HttpGet("user")]
        public async Task<Guid> GetCurrentUserOfficeId()
        {
            return await this.officeLogic.GetCurrentUserOfficeId();
        }

        [HttpPost("user")]
        public async Task<bool> SaveCurrentUserOfficeId([FromBody]Guid selectedOfficeId)
        {
            return await this.officeLogic.SaveCurrentUserOfficeId(selectedOfficeId);
        }
    }
}
