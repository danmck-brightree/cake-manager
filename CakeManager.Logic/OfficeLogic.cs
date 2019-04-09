using AutoMapper.QueryableExtensions;
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
    public class OfficeLogic : BaseLogic, IOfficeLogic
    {
        private readonly ICakeMarkDbContext cakeMarkDbContext;
        private readonly IAccountLogic accountLogic;

        public OfficeLogic(ICakeMarkDbContext cakeMarkDbContext, IHttpContextAccessor httpContext, IAccountLogic accountLogic)
            : base(cakeMarkDbContext, httpContext)
        {
            this.cakeMarkDbContext = cakeMarkDbContext;
            this.accountLogic = accountLogic;
        }

        public async Task<List<Office>> GetOffices()
        {
            try
            {
                var selectedOfficeId = await GetCurrentUserOfficeId();

                var offices = await this.cakeMarkDbContext.Office
                    .ProjectTo<Office>()
                    .ToListAsync();

                if (selectedOfficeId != default)
                    offices.Single(x => x.Id == selectedOfficeId).Selected = true;

                return offices;
            }
            catch
            {
                return new List<Office>();
            }
        }

        public async Task<Guid> GetCurrentUserOfficeId()
        {
            try
            {
                if (!CurrentUserId.HasValue)
                    return default;

                var selectedOfficeId = await this.cakeMarkDbContext.Office
                    .Where(x => x.Users.Any(y => y.Id == CurrentUserId.Value))
                    .Select(x => x.Id)
                    .FirstOrDefaultAsync();

                return selectedOfficeId;
            }
            catch
            {
                return default;
            }
        }

        public async Task<bool> SaveCurrentUserOfficeId(Guid selectedOfficeId)
        {
            if (selectedOfficeId == default)
                return false;

            try
            {

                if (!this.CurrentUserId.HasValue)
                {
                    if (!await this.accountLogic.RegisterLocalUser(selectedOfficeId))
                        return false;
                }
                else
                {
                    var user = await this.cakeMarkDbContext.ActiveDirectoryUser
                        .SingleAsync(x => x.Id == CurrentUserId.Value);

                    user.OfficeId = selectedOfficeId;

                    await this.cakeMarkDbContext.SaveChangesAsync();
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteOffice(Guid officeId)
        {
            try
            {
                var office = await this.cakeMarkDbContext.Office
                    .FirstOrDefaultAsync(x => x.Id == officeId);

                if (office == null)
                    return false;

                if (office.Users != null && office.Users.Any())
                    return false;

                this.cakeMarkDbContext.Office.Remove(office);

                return (await this.cakeMarkDbContext.SaveChangesAsync()) > 0;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> EditOffice(Office office)
        {
            try
            {
                Repository.Models.Office dbOffice;

                if (office.Id.HasValue)
                {
                    dbOffice = await this.cakeMarkDbContext.Office
                        .FirstOrDefaultAsync(x => x.Id == office.Id.Value);

                    dbOffice.Name = office.Name;

                    return (await this.cakeMarkDbContext.SaveChangesAsync()) > 0;
                }
                else
                {
                    dbOffice = AutoMapper.Mapper.Map<Repository.Models.Office>(office);

                    this.cakeMarkDbContext.Office.Add(dbOffice);

                    return (await this.cakeMarkDbContext.SaveChangesAsync()) > 0;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
