using CakeManager.Repository.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CakeManager.Repository
{
    public interface ICakeMarkDbContext
    {
        DbSet<CakeMark> CakeMark { get; set; }
        DbSet<Office> Office { get; set; }
        DbSet<TempUser> TempUser { get; set; }
        DbSet<TempUserToken> TempUserToken { get; set; }
        Task<int> SaveChangesAsync();
    }
}
