using CakeManager.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CakeManager.Logic
{
    public interface ICakeMarkLogic
    {
        Task<int> GetCakeMarkTally();
        Task<int> GetSuperCakeMarkTally();
        Task<bool> AddCakeMark();
        Task<bool> RemoveCakeMark();
        Task<bool> RemoveSuperCakeMark();
        Task<List<CakeMarkGridData>> GetCakeMarkGridData(Guid officeId);
    }
}
