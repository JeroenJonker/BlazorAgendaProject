using BlazorAgenda.Services.Interfaces;
using BlazorAgenda.Shared.Models;
using BlazorAgenda.Shared.Properties;
using Microsoft.AspNetCore.Blazor;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace BlazorAgenda.Services
{
    public class EventService : DefaultObjectService<Event>, IEventService
    {
        public EventService(HttpClient client) : base(client)
        {
        }

        public async Task<List<Event>> GetEvents(User user)
        {
            return await http.GetJsonAsync<List<Event>>(Resources.EventApi_GetUserEvents + user.Id);
        }
    }
}
