using AutoMapper.QueryableExtensions;
using CakeManager.Repository;
using CakeManager.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CakeManager.Logic
{
    public class AccountLogic : BaseLogic, IAccountLogic
    {
        private readonly ICakeMarkDbContext cakeMarkDbContext;

        public AccountLogic(ICakeMarkDbContext cakeMarkDbContext, IHttpContextAccessor httpContext)
            : base(cakeMarkDbContext, httpContext)
        {
            this.cakeMarkDbContext = cakeMarkDbContext;
        }

        public async Task<bool> HasLocalUser()
        {
            try
            {
                if (!CurrentUserId.HasValue)
                    return false;

                var user = await this.cakeMarkDbContext.ActiveDirectoryUser
                    .FirstOrDefaultAsync(x => x.Id == CurrentUserId.Value);

                return user != null;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> RegisterLocalUser(Guid selectedOfficeId)
        {
            try
            {
                var currentUserEmail = this.CurrentUserEmail;

                var currentUser = await this.cakeMarkDbContext.ActiveDirectoryUser
                    .FirstOrDefaultAsync(x => x.Email == currentUserEmail);

                if (currentUser == null)
                {
                    currentUser = new Repository.Models.ActiveDirectoryUser
                    {
                        Email = currentUserEmail,
                        OfficeId = selectedOfficeId
                    };
                    this.cakeMarkDbContext.ActiveDirectoryUser.Add(currentUser);
                    this.cakeMarkDbContext.SaveChanges();

                    return true;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<ActiveDirectoryUser>> GetUsers()
        {
            try
            {
                var users = await this.cakeMarkDbContext.ActiveDirectoryUser
                    .ProjectTo<ActiveDirectoryUser>()
                    .ToListAsync();

                var offices = await this.cakeMarkDbContext.Office
                    .ToDictionaryAsync(x => x.Id, x => x.Name);

                users.ForEach(x =>
                {
                    x.Office = offices[x.OfficeId];
                });

                return users;
            }
            catch
            {
                return new List<ActiveDirectoryUser>();
            }
        }

        public async Task<bool> DeleteUser(string email)
        {
            try
            {
                var user = await this.cakeMarkDbContext.ActiveDirectoryUser
                    .FirstOrDefaultAsync(x => x.Email == email);

                if (user == null)
                    return false;

                this.cakeMarkDbContext.ActiveDirectoryUser.Remove(user);

                return (await this.cakeMarkDbContext.SaveChangesAsync()) > 0;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> ToggleUserAdmin(string email)
        {
            try
            {
                var user = await this.cakeMarkDbContext.ActiveDirectoryUser
                       .FirstOrDefaultAsync(x => x.Email == email);

                if (user == null)
                    return false;

                user.IsAdmin = !user.IsAdmin;

                return (await this.cakeMarkDbContext.SaveChangesAsync()) > 0;
            }
            catch
            {
                return false;
            }
        }
    }
}
