using CakeManager.Repository;
using CakeManager.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CakeManager.Logic
{
    public class CakeMarkLogic : BaseLogic, ICakeMarkLogic
    {
        private readonly ICakeMarkDbContext cakeMarkDbContext;

        public CakeMarkLogic(ICakeMarkDbContext cakeMarkDbContext, IHttpContextAccessor httpContext)
            : base(cakeMarkDbContext, httpContext)
        {
            this.cakeMarkDbContext = cakeMarkDbContext;
        }

        public async Task<int> GetCakeMarkTally()
        {
            try
            {
                return await this.cakeMarkDbContext.CakeMark
                    .Where(x => x.UserId == this.CurrentUserId.Value)
                    .CountAsync();
            }
            catch
            {
                return 0;
            }
        }

        public async Task<int> GetSuperCakeMarkTally()
        {
            try
            {
                return await this.cakeMarkDbContext.SuperCakeMark
                    .Where(x => x.UserId == this.CurrentUserId.Value)
                    .CountAsync();
            }
            catch
            {
                return 0;
            }
        }
        
        public async Task<bool> AddCakeMark()
        {
            try
            {
                var existingCakeMarks = this.cakeMarkDbContext.CakeMark
                    .Where(x => x.CreatedBy == this.CurrentUserId.Value)
                    .Count();

                if (existingCakeMarks + 1 == Constants.CakeMarkTallyMax)
                {
                    var existingSuperCakeMarks = this.cakeMarkDbContext.SuperCakeMark
                        .Where(x => x.CreatedBy == this.CurrentUserId.Value)
                        .Count();

                    if (existingSuperCakeMarks == Constants.SuperCakeMarkTallyMax)
                        return true;

                    var dbSuperCakeMark = new Repository.Models.SuperCakeMark
                    {
                        UserId = this.CurrentUserId.Value,
                        CreatedBy = this.CurrentUserId.Value,
                        CreatedDate = DateTime.UtcNow
                    };

                    await this.cakeMarkDbContext.SuperCakeMark.AddAsync(dbSuperCakeMark);

                    var officeId = this.cakeMarkDbContext.ActiveDirectoryUser
                        .Where(x => x.Id == this.CurrentUserId.Value)
                        .Select(x => x.OfficeId)
                        .FirstOrDefault();

                    this.cakeMarkDbContext.CakeMark
                        .RemoveRange(this.cakeMarkDbContext.CakeMark
                            .Where(x => x.User.OfficeId == officeId));

                    return (await this.cakeMarkDbContext.SaveChangesAsync()) > 0;
                }

                var dbCakeMark = new Repository.Models.CakeMark
                {
                    UserId = this.CurrentUserId.Value,
                    CreatedDate = DateTime.UtcNow,
                    CreatedBy = this.CurrentUserId.Value,
                };

                await this.cakeMarkDbContext.CakeMark.AddAsync(dbCakeMark);

                return (await this.cakeMarkDbContext.SaveChangesAsync()) > 0;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> RemoveCakeMark()
        {
            try
            {
                var dbCakeMark = this.cakeMarkDbContext.CakeMark
                    .Where(x => x.UserId == this.CurrentUserId.Value)
                    .OrderByDescending(x => x.CreatedDate)
                    .FirstOrDefault();

                if (dbCakeMark == null)
                    return false;

                this.cakeMarkDbContext.CakeMark.Remove(dbCakeMark);

                var result = await this.cakeMarkDbContext.SaveChangesAsync();

                return result > 0;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> RemoveSuperCakeMark()
        {
            try
            {
                var dbCakeMark = this.cakeMarkDbContext.SuperCakeMark
                    .Where(x => x.UserId == this.CurrentUserId.Value)
                    .OrderByDescending(x => x.CreatedDate)
                    .FirstOrDefault();

                if (dbCakeMark == null)
                    return false;

                this.cakeMarkDbContext.SuperCakeMark.Remove(dbCakeMark);

                var result = await this.cakeMarkDbContext.SaveChangesAsync();

                return result > 0;
            }
            catch
            {
                return false;
            }
        }
        
        public async Task<List<CakeMarkGridData>> GetCakeMarkGridData(Guid officeId)
        {
            try
            {
                return await this.cakeMarkDbContext.ActiveDirectoryUser
                    .Where(x => x.OfficeId == officeId)
                    .Select(x => new CakeMarkGridData
                    {
                        Name = x.Email,
                        CakeMarks = x.CakeMarks.Count(),
                        SuperCakeMarks = x.SuperCakeMarks.Count()
                    })
                    .ToListAsync();
            }
            catch
            {
                return new List<CakeMarkGridData>();
            }
        }
    }
}
