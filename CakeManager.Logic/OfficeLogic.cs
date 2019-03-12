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
    public class OfficeLogic : IOfficeLogic
    {
        private readonly ICakeMarkDbContext cakeMarkDbContext;

        private Guid? currentUserId = null;

        public OfficeLogic(ICakeMarkDbContext cakeMarkDbContext)
        {
            this.cakeMarkDbContext = cakeMarkDbContext;
            this.currentUserId = Constants.TemporaryUserId;
        }

        public async Task<List<Office>> GetOffices()
        {
            try
            {
                var selectedOfficeId = await this.cakeMarkDbContext.Office
                    .Where(x => x.Users.Any(y => y.Id == currentUserId.Value))
                    .Select(x => x.Id)
                    .FirstOrDefaultAsync();

                var offices = await this.cakeMarkDbContext.Office
                    .ProjectTo<Office>()
                    .ToListAsync();

                var selectedOffice = offices.FirstOrDefault(x => x.Id == selectedOfficeId);
                selectedOffice.Selected = true;

                return offices;
            }
            catch
            {
                return new List<Office>();
            }
        }
    }
}
