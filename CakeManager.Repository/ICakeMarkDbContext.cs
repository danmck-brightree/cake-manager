using CakeManager.Repository.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CakeManager.Repository
{
    public interface ICakeMarkDbContext
    {
        DbSet<CakeMark> CakeMark { get; set; }
        Task<int> SaveChangesAsync();
    }
}
