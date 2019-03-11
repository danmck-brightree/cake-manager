using CakeManager.Shared;
using System.Threading.Tasks;

namespace CakeManager.Client.Services.Interfaces
{
    public interface ICakeMarkService
    {
        Task<int> GetCakeMarkTally();
        Task<bool> AddCakeMark(CakeMark cakeMark);
        Task<bool> RemoveCakeMark(CakeMark cakeMark);
    }
}
