using CakeManager.Repository;
using CakeManager.Repository.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq;
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
                var user = await this.cakeMarkDbContext.ActiveDirectoryUser
                    .SingleAsync(x => x.Id == CurrentUserId.Value);

                return user != null;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> RegisterLocalUser()
        {
            try
            {
                var currentUserEmail = this.CurrentUserEmail;

                var currentUser = await this.cakeMarkDbContext.ActiveDirectoryUser
                    .FirstOrDefaultAsync(x => x.Email == currentUserEmail);

                if (currentUser == null)
                {
                    currentUser = new ActiveDirectoryUser
                    {
                        Email = currentUserEmail
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
    }
}
