using CakeManager.Repository;
using CakeManager.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CakeManager.Logic
{
    public class CakeMarkLogic : BaseLogic, ICakeMarkLogic
    {
        private readonly ICakeMarkDbContext cakeMarkDbContext;
        private readonly IOfficeLogic officeLogic;
        

        public CakeMarkLogic(ICakeMarkDbContext cakeMarkDbContext, IHttpContextAccessor httpContext, IOfficeLogic officeLogic)
            : base(cakeMarkDbContext, httpContext)
        {
            this.cakeMarkDbContext = cakeMarkDbContext;
            this.officeLogic = officeLogic;
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
        
        public async Task<CakeMarkResult> AddCakeMark(DateTime latestEventDate)
        {
            try
            {
                if (await CheckDataExpired(latestEventDate))
                    return new CakeMarkResult
                    {
                        Success = false,
                        Status = CakeMarkResult.StatusType.ExpiredData
                    };

                var existingCakeMarks = this.cakeMarkDbContext.CakeMark
                    .Where(x => x.CreatedBy == this.CurrentUserId.Value)
                    .Count();

                if (existingCakeMarks + 1 == Constants.CakeMarkTallyMax)
                {
                    var existingSuperCakeMarks = this.cakeMarkDbContext.SuperCakeMark
                        .Where(x => x.CreatedBy == this.CurrentUserId.Value)
                        .Count();

                    if (existingSuperCakeMarks == Constants.SuperCakeMarkTallyMax)
                        return new CakeMarkResult
                        {
                            Success = true,
                            Status = CakeMarkResult.StatusType.LimitReached
                        };

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

                    return new CakeMarkResult
                    {
                        Success = (await this.cakeMarkDbContext.SaveChangesAsync()) > 0
                    };
                }

                var dbCakeMark = new Repository.Models.CakeMark
                {
                    UserId = this.CurrentUserId.Value,
                    CreatedDate = DateTime.UtcNow,
                    CreatedBy = this.CurrentUserId.Value,
                };

                await this.cakeMarkDbContext.CakeMark.AddAsync(dbCakeMark);

                var success = (await this.cakeMarkDbContext.SaveChangesAsync()) > 0;
                return new CakeMarkResult
                {
                    Success = success
                };
            }
            catch
            {
                return new CakeMarkResult
                {
                    Success = false,
                    Status = CakeMarkResult.StatusType.Exception
                };
            }
        }

        public async Task<CakeMarkResult> RemoveCakeMark(DateTime latestEventDate)
        {
            try
            {
                if (await CheckDataExpired(latestEventDate))
                    return new CakeMarkResult
                    {
                        Success = false,
                        Status = CakeMarkResult.StatusType.ExpiredData
                    };

                var dbCakeMark = this.cakeMarkDbContext.CakeMark
                    .Where(x => x.UserId == this.CurrentUserId.Value)
                    .OrderByDescending(x => x.CreatedDate)
                    .FirstOrDefault();

                if (dbCakeMark == null)
                    return new CakeMarkResult
                    {
                        Success = false,
                        Status = CakeMarkResult.StatusType.ExpiredData
                    };

                dbCakeMark.IsDeleted = true;

                return new CakeMarkResult
                {
                    Success = (await this.cakeMarkDbContext.SaveChangesAsync()) > 0
                };
            }
            catch
            {
                return new CakeMarkResult
                {
                    Success = false,
                    Status = CakeMarkResult.StatusType.Exception
                };
            }
        }

        public async Task<CakeMarkResult> RemoveSuperCakeMark(DateTime latestEventDate)
        {
            try
            {
                if (await CheckDataExpired(latestEventDate))
                    return new CakeMarkResult
                    {
                        Success = false,
                        Status = CakeMarkResult.StatusType.ExpiredData
                    };

                var dbCakeMark = this.cakeMarkDbContext.SuperCakeMark
                    .Where(x => x.UserId == this.CurrentUserId.Value)
                    .OrderByDescending(x => x.CreatedDate)
                    .FirstOrDefault();

                if (dbCakeMark == null)
                    return new CakeMarkResult
                    {
                        Success = false,
                        Status = CakeMarkResult.StatusType.ExpiredData
                    };

                dbCakeMark.IsDeleted = true;

                return new CakeMarkResult
                {
                    Success = (await this.cakeMarkDbContext.SaveChangesAsync()) > 0
                };
            }
            catch
            {
                return new CakeMarkResult
                {
                    Success = false,
                    Status = CakeMarkResult.StatusType.Exception
                };
            }
        }

        private async Task<bool> CheckDataExpired(DateTime latestEventDate)
        {
            var officeId = await officeLogic.GetCurrentUserOfficeId();
            if (officeId == Guid.Empty)
                return true;

            var gridData = await GetCakeMarkGridData(officeId);
            if (gridData == null)
                return true;

            return latestEventDate != gridData.LatestEventDate;
        }
        
        public async Task<CakeMarkGridData> GetCakeMarkGridData(Guid officeId)
        {
            try
            {
                var cakeMarkGridDataItems = await this.cakeMarkDbContext.ActiveDirectoryUser
                    .Where(x => x.OfficeId == officeId)
                    .Select(x => new CakeMarkGridDataItem
                    {
                        Name = x.Email,
                        CakeMarks = x.CakeMarks.Where(y => !y.IsDeleted).Count(),
                        SuperCakeMarks = x.SuperCakeMarks.Where(y => !y.IsDeleted).Count(),
                        LatestEventDate = new[]
                        {
                            x.CakeMarks.Select(y => y.CreatedDate).DefaultIfEmpty(DateTime.MinValue).Max(),
                            x.SuperCakeMarks.Select(y => y.CreatedDate).DefaultIfEmpty(DateTime.MinValue).Max()
                        }
                        .Max()
                    })
                    .ToListAsync();

                var data = new CakeMarkGridData
                {
                    Items = cakeMarkGridDataItems,
                    LatestEventDate = cakeMarkGridDataItems.Max(x => x.LatestEventDate)
                };

                return data;
            }
            catch
            {
                return new CakeMarkGridData();
            }
        }
    }
}
