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
        Task<CakeMarkResult> AddCakeMark(DateTime latestEventDate);
        Task<CakeMarkResult> RemoveCakeMark(DateTime latestEventDate);
        Task<CakeMarkResult> RemoveSuperCakeMark(DateTime latestEventDate);
        Task<CakeMarkGridData> GetCakeMarkGridData(Guid officeId);
    }
}
