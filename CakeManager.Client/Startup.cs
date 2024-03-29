using CakeManager.Client.Utilities;
using CakeManager.Client.Services;
using CakeManager.Client.Services.Interfaces;
using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace CakeManager.Client
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ITokenService, TokenService>();
            services.AddSingleton<ICakeMarkService, CakeMarkService>();
            services.AddSingleton<IOfficeService, OfficeService>();
            services.AddSingleton<IUserService, UserService>();

            services.AddScoped<ITokenHttpClient, TokenHttpClient>();
            services.AddScoped<IToastService, ToastService>();
        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
