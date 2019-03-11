using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using CakeManager.Repository.Models;
using Microsoft.EntityFrameworkCore;

namespace CakeManager.Repository
{
    public class CakeMarkDbContext : DbContext, ICakeMarkDbContext
    {
        public CakeMarkDbContext(DbContextOptions<CakeMarkDbContext> options) : base (options)
        {
        }

        public DbSet<CakeMark> CakeMark { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }
    }
}
