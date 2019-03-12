using CakeManager.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CakeManager.Logic
{
    public interface ICakeMarkLogic
    {
        Task<int> GetCakeMarkTally();
        Task<bool> AddCakeMark(CakeMark cakeMark);
        Task<bool> RemoveCakeMark(Guid userId);
        Task<List<CakeMarkGridData>> GetCakeMarkGridData(Guid officeId);
    }
}
