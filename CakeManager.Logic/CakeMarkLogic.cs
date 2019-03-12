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
    public class CakeMarkLogic : ICakeMarkLogic
    {
        private readonly ICakeMarkDbContext cakeMarkDbContext;

        private Guid? currentUserId = null;

        public CakeMarkLogic(ICakeMarkDbContext cakeMarkDbContext, IHttpContextAccessor httpContext)
        {
            this.cakeMarkDbContext = cakeMarkDbContext;
            this.currentUserId = Constants.TemporaryUserId;
        }

        public async Task<int> GetCakeMarkTally()
        {
            try
            {
                return await this.cakeMarkDbContext.CakeMark
                    .Where(x => x.UserId == this.currentUserId.Value)
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
                    .Where(x => x.UserId == this.currentUserId.Value)
                    .CountAsync();
            }
            catch
            {
                return 0;
            }
        }
        
        public async Task<bool> AddCakeMark(CakeMark cakeMark)
        {
            try
            {
                var existingCakeMarks = this.cakeMarkDbContext.CakeMark
                    .Where(x => x.CreatedBy == this.currentUserId.Value)
                    .Count();

                if (existingCakeMarks + 1 == Constants.CakeMarkTallyMax)
                {
                    var existingSuperCakeMarks = this.cakeMarkDbContext.SuperCakeMark
                        .Where(x => x.CreatedBy == this.currentUserId.Value)
                        .Count();

                    if (existingSuperCakeMarks == Constants.SuperCakeMarkTallyMax)
                        return true;

                    var dbSuperCakeMark = new Repository.Models.SuperCakeMark
                    {
                        UserId = cakeMark.UserId,
                        CreatedBy = this.currentUserId.Value,
                        CreatedDate = DateTime.UtcNow
                    };

                    await this.cakeMarkDbContext.SuperCakeMark.AddAsync(dbSuperCakeMark);

                    var officeId = this.cakeMarkDbContext.TempUser
                        .Where(x => x.Id == this.currentUserId.Value)
                        .Select(x => x.OfficeId)
                        .FirstOrDefault();

                    this.cakeMarkDbContext.CakeMark
                        .RemoveRange(this.cakeMarkDbContext.CakeMark
                            .Where(x => x.User.OfficeId == officeId));

                    return (await this.cakeMarkDbContext.SaveChangesAsync()) > 0;
                }

                var dbCakeMark = AutoMapper.Mapper.Map<Repository.Models.CakeMark>(cakeMark);

                dbCakeMark.CreatedDate = DateTime.UtcNow;
                dbCakeMark.CreatedBy = this.currentUserId.Value;

                await this.cakeMarkDbContext.CakeMark.AddAsync(dbCakeMark);

                return (await this.cakeMarkDbContext.SaveChangesAsync()) > 0;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> RemoveCakeMark(Guid userId)
        {
            try
            {
                var dbCakeMark = this.cakeMarkDbContext.CakeMark
                    .Where(x => x.UserId == userId)
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

        public async Task<bool> RemoveSuperCakeMark(Guid userId)
        {
            try
            {
                var dbCakeMark = this.cakeMarkDbContext.SuperCakeMark
                    .Where(x => x.UserId == userId)
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
                return await this.cakeMarkDbContext.TempUser
                    .Where(x => x.OfficeId == officeId)
                    .Select(x => new CakeMarkGridData
                    {
                        Name = x.Name,
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
