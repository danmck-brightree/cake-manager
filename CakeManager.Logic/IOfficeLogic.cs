using CakeManager.Shared;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CakeManager.Logic
{
    public interface IOfficeLogic
    {
        Task<List<Office>> GetOffices();
    }
}
