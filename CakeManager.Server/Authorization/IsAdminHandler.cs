using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CakeManager.Repository;
using Microsoft.AspNetCore.Authorization;

namespace CakeManager.Server.Authorization
{
    public class IsAdminHandler : AuthorizationHandler<IsAdminRequirement>
    {
        private readonly ICakeMarkDbContext dbContext;

        public IsAdminHandler(ICakeMarkDbContext dbContext) {
            this.dbContext = dbContext;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, IsAdminRequirement requirement)
        {
            var email = context.User.Claims
                .FirstOrDefault(x => x.Type == ClaimTypes.Name)
                ?.Value;

            if (email == null)
            {
                context.Fail();
                return;
            }

            var user = dbContext.ActiveDirectoryUser.SingleOrDefault(x => x.Email == email);

            if (user == null)
            {
                context.Fail();
                return;
            }

            if (user.IsAdmin)
                context.Succeed(requirement);
            else
                context.Fail();

            await Task.CompletedTask;
        }
    }
}
