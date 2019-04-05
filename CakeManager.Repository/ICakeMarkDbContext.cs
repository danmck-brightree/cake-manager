using CakeManager.Repository.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CakeManager.Repository
{
    public interface ICakeMarkDbContext
    {
        DbSet<CakeMark> CakeMark { get; set; }
        DbSet<Office> Office { get; set; }
        DbSet<SuperCakeMark> SuperCakeMark { get; set; }
        DbSet<ActiveDirectoryUser> ActiveDirectoryUser { get; set; }
        Task<int> SaveChangesAsync();
        int SaveChanges();
    }
}
