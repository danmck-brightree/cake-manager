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
        public DbSet<TempUser> TempUser { get; set; }
        public DbSet<Models.SuperCakeMark> SuperCakeMark { get; set; }

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

            var me = new TempUser
            {
                Id = Constants.TemporaryUserId,
                Name = "Daniel McKenzie",
                Email = "dmckenzie@brightree.com",
                OfficeId = aberdeenOffice.Id,
                Password = "temp"
            };

            builder.Entity<TempUser>().HasData(
                me,
                new TempUser
                {
                    Id = Guid.NewGuid(),
                    Name = "John Smith",
                    Email = "jsmith@brightree.com",
                    OfficeId = aberdeenOffice.Id,
                    Password = "temp"
                },
                new TempUser
                {
                    Id = Guid.NewGuid(),
                    Name = "Test Person",
                    Email = "tperson@brightree.com",
                    OfficeId = glasgowOffice.Id,
                    Password = "temp"
                });
        }

        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }
    }
}
