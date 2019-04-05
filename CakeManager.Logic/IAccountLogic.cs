using System.Threading.Tasks;

namespace CakeManager.Logic
{
    public interface IAccountLogic
    {
        Task<bool> HasLocalUser();
        Task<bool> RegisterLocalUser();
    }
}
