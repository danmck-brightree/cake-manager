using CakeManager.Repository;
using CakeManager.Repository.Models;
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

        private static object userCreateLock = new object();

        private Guid? currentUserId = null;

        protected Guid? CurrentUserId
        {
            get
            {
                if (this.currentUserId == null)
                {
                    lock (userCreateLock)
                    {
                        try
                        {
                            var currentUserEmail = httpContext.HttpContext.User.Claims
                                .FirstOrDefault(x => x.Type == ClaimTypes.Name)
                                ?.Value;

                            if (currentUserEmail != null)
                            {
                                var currentUser = this.cakeMarkDbContext.ActiveDirectoryUser
                                    .FirstOrDefault(x => x.Email == currentUserEmail);

                                if (currentUser == null)
                                {
                                    currentUser = new ActiveDirectoryUser
                                    {
                                        Email = currentUserEmail
                                    };
                                    this.cakeMarkDbContext.ActiveDirectoryUser.Add(currentUser);
                                    this.cakeMarkDbContext.SaveChanges();
                                }

                                this.currentUserId = currentUser.Id;
                            }
                        }
                        catch
                        {
                            this.currentUserId = null;
                        }
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
