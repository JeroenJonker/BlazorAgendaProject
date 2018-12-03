using Microsoft.AspNetCore.Blazor.Builder;
using Microsoft.Extensions.DependencyInjection;
using BlazorAgenda.Client.Viewmodels;
using BlazorAgenda.Services;
using BlazorAgenda.Shared;
using BlazorAgenda.Services.Interfaces;

namespace BlazorAgenda.Client
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<IEventService, EventService>();
        }

        public void Configure(IBlazorApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
