using System;
using System.Threading.Tasks;
using CakeManager.Repository.Models;
using CakeManager.Shared;
using Microsoft.EntityFrameworkCore;

namespace CakeManager.Repository
{
    public class CakeMarkDbContext : DbContext, ICakeMarkDbContext
    {
        public CakeMarkDbContext(DbContextOptions<CakeMarkDbContext> options) : base (options)
        {
        }

        public DbSet<Models.CakeMark> CakeMark { get; set; }
        public DbSet<Models.Office> Office { get; set; }
        public DbSet<Models.SuperCakeMark> SuperCakeMark { get; set; }
        public DbSet<ActiveDirectoryUser> ActiveDirectoryUser { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var aberdeenOffice = new Models.Office
            {
                Id = Guid.NewGuid(),
                Name = "Aberdeen"
            };

            var glasgowOffice = new Models.Office
            {
                Id = Guid.NewGuid(),
                Name = "Glasgow"
            };

            builder.Entity<Models.Office>().HasData(
                aberdeenOffice,
                glasgowOffice);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }
    }
}
