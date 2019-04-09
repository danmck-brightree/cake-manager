using AutoMapper;
using CakeManager.Logic;
using CakeManager.Repository;
using CakeManager.Server.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;

namespace CakeManager.Server
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("DefaultConnection");

            services
                .AddMvc(o =>
                {
                    o.Filters.Add(new AuthorizeFilter("default"));
                })
                .AddNewtonsoftJson();

            services.AddAuthorization(o =>
            {
                o.AddPolicy(Policies.Default, policy =>
                {
                    policy.RequireAuthenticatedUser();
                });
                o.AddPolicy(Policies.IsAdmin, policy =>
                {
                    policy.Requirements.Add(new IsAdminRequirement());
                });
            });

            services
                .AddAuthentication(o =>
                {
                    o.DefaultScheme = "Bearer";
                })
                .AddJwtBearer(o =>
                {
                    o.Authority = Configuration["Authentication:Authority"];
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidAudiences = new List<string>
                        {
                            Configuration["Authentication:AppIdUri"],
                            Configuration["Authentication:ClientId"]
                        }
                    };
                });

            services.AddResponseCompression();
            services.AddDbContext<CakeMarkDbContext>(options => options.UseSqlServer(connectionString, b => b.MigrationsAssembly("CakeManager.Server")));

            Mapper.Initialize(x => x.AddProfile<AutoMapperProfile>());

            services.AddScoped<ICakeMarkDbContext, CakeMarkDbContext>();
            services.AddScoped<ICakeMarkLogic, CakeMarkLogic>();
            services.AddScoped<IOfficeLogic, OfficeLogic>();
            services.AddScoped<IAccountLogic, AccountLogic>();
            services.AddScoped<IAuthorizationHandler, IsAdminHandler>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseResponseCompression();

            if (env.EnvironmentName == "Development")
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(name: "default", template: "{controller}/{action}/{id?}");
            });

            app.UseBlazor<Client.Startup>();
            app.UseBlazorDebugging();
        }
    }
}
