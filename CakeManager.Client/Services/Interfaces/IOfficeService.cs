using CakeManager.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CakeManager.Client.Services.Interfaces
{
    public interface IOfficeService
    {
        Task<List<Office>> GetOffices();
        Task<Guid> GetCurrentUserOfficeId();
        Task<bool> SaveCurrentUserOfficeId(Guid selectedOfficeId);
        Task<bool> EditOffice(Office office);
        Task<bool> DeleteOffice(Guid officeId);
    }
}
