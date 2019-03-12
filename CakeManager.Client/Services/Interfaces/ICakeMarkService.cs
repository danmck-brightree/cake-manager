using CakeManager.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CakeManager.Client.Services.Interfaces
{
    public interface ICakeMarkService
    {
        Task<int> GetCakeMarkTally();
        Task<bool> AddCakeMark(CakeMark cakeMark);
        Task<bool> RemoveCakeMark(CakeMark cakeMark);
        Task<List<CakeMarkGridData>> GetCakeMarkGridData(Guid selectedOfficeId);
    }
}
