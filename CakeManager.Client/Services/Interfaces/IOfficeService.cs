using CakeManager.Shared;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CakeManager.Client.Services.Interfaces
{
    public interface IOfficeService
    {
        Task<List<Office>> GetOffices();
    }
}
