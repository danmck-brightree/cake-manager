using AutoMapper;
using CakeManager.Logic;
using CakeManager.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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

            services.AddMvc().AddNewtonsoftJson();
            services.AddResponseCompression();
            services.AddDbContext<CakeMarkDbContext>(options => options.UseSqlServer(connectionString, b => b.MigrationsAssembly("CakeManager.Server")));

            Mapper.Initialize(x => x.AddProfile<AutoMapperProfile>());

            services.AddScoped<ICakeMarkDbContext, CakeMarkDbContext>();
            services.AddScoped<ICakeMarkLogic, CakeMarkLogic>();
            services.AddScoped<IOfficeLogic, OfficeLogic>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseResponseCompression();

            if (env.EnvironmentName == "Development")
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc(routes =>
            {
                routes.MapRoute(name: "default", template: "{controller}/{action}/{id?}");
            });

            app.UseBlazor<Client.Startup>();
            app.UseBlazorDebugging();
        }
    }
}
