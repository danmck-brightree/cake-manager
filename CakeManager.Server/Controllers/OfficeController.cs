using CakeManager.Logic;
using CakeManager.Shared;
using Microsoft.AspNetCore.Mvc;
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
    }
}
