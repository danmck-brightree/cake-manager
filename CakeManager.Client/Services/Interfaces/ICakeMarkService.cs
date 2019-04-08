using CakeManager.Shared;
using System;
using System.Threading.Tasks;

namespace CakeManager.Client.Services.Interfaces
{
    public interface ICakeMarkService
    {
        Task<int> GetCakeMarkTally();
        Task<int> GetSuperCakeMarkTally();
        Task<CakeMarkResult> AddCakeMark(DateTime latestEventDate);
        Task<CakeMarkResult> RemoveCakeMark(DateTime latestEventDate);
        Task<CakeMarkResult> RemoveSuperCakeMark(DateTime latestEventDate);
        Task<CakeMarkGridData> GetCakeMarkGridData(Guid selectedOfficeId);
    }
}
