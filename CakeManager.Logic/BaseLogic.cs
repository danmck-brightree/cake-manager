using CakeManager.Repository;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Security.Claims;

namespace CakeManager.Logic
{
    public class BaseLogic
    {
        private readonly ICakeMarkDbContext cakeMarkDbContext;
        private readonly IHttpContextAccessor httpContext;

        protected string CurrentUserEmail
        {
            get
            {
                return httpContext.HttpContext.User.Claims
                    .FirstOrDefault(x => x.Type == ClaimTypes.Name)
                    ?.Value;
            }
        }

        private Guid? currentUserId = null;

        protected Guid? CurrentUserId
        {
            get
            {
                if (this.currentUserId == null)
                {
                    try
                    {
                        var currentUserEmail = CurrentUserEmail;

                        if (currentUserEmail != null)
                        {
                            var currentUser = this.cakeMarkDbContext.ActiveDirectoryUser
                                .FirstOrDefault(x => x.Email == currentUserEmail);

                            if (currentUser != null)
                                this.currentUserId = currentUser.Id;
                        }
                    }
                    catch
                    {
                        this.currentUserId = null;
                    }
                }

                return this.currentUserId;
            }
        }

        public BaseLogic(ICakeMarkDbContext cakeMarkDbContext, IHttpContextAccessor httpContext)
        {
            this.cakeMarkDbContext = cakeMarkDbContext;
            this.httpContext = httpContext;
        }
    }
}
