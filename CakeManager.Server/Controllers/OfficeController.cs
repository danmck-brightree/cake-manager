using CakeManager.Logic;
using CakeManager.Server.Authorization;
using CakeManager.Shared;
using Microsoft.AspNetCore.Authorization;
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

        [HttpPost("deleteoffice")]
        [Authorize(Policy = Policies.IsAdmin)]
        public async Task<bool> DeleteOffice([FromBody]Guid officeId)
        {
            return await this.officeLogic.DeleteOffice(officeId);
        }

        [HttpPost("editoffice")]
        [Authorize(Policy = Policies.IsAdmin)]
        public async Task<bool> EditOffice([FromBody]Office office)
        {
            return await this.officeLogic.EditOffice(office);
        }
    }
}
