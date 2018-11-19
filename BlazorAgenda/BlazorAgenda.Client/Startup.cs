using Microsoft.AspNetCore.Blazor.Builder;
using Microsoft.Extensions.DependencyInjection;
using BlazorAgenda.Client.Viewmodels;
using BlazorAgenda.Services;
using BlazorAgenda.Shared;

namespace BlazorAgenda.Client
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<CalendarEventService>();
            services.AddSingleton<CalendarService>();
        }

        public void Configure(IBlazorApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
