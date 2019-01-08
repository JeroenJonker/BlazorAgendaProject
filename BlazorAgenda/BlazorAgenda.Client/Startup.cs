using Microsoft.AspNetCore.Blazor.Builder;
using Microsoft.Extensions.DependencyInjection;
using BlazorAgenda.Services;
using BlazorAgenda.Services.Interfaces;
using BlazorAgenda.Shared.Models;
using BlazorAgenda.Client.Services;
using BlazorAgenda.Shared.Interfaces;

namespace BlazorAgenda.Client
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<IEventService, EventService>();
            services.AddSingleton<IStateService, StateService>();
            services.AddSingleton<EventViewService>();
            services.AddSingleton<UserViewService>();
            services.AddTransient<IUser, User>();
            services.AddTransient<IEvent, Event>();
        }

        public void Configure(IBlazorApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
