using CakeManager.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CakeManager.Logic
{
    public interface IOfficeLogic
    {
        Task<List<Office>> GetOffices();
        Task<Guid> GetCurrentUserOfficeId();
        Task<bool> SaveCurrentUserOfficeId(Guid selectedOfficeId);
        Task<bool> DeleteOffice(Guid officeId);
        Task<bool> EditOffice(Office office);
    }
}
