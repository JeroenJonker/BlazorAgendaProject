using BlazorAgenda.Services.Interfaces;
using BlazorAgenda.Shared.Models;
using Microsoft.AspNetCore.Blazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAgenda.Services
{
    public class EventService : DefaultObjectService<Event>, IEventService
    {
        public EventService(HttpClient client) : base(client)
        {

        }

        public async Task ExecuteAsync(Event CurrentEvent)
        {
            if (GetObjectState(CurrentEvent) == ObjectState.Edit)
            {
                await http.PutJsonAsync("api/Event/Edit", CurrentEvent);
            }
            else
            {
                await http.PostJsonAsync("api/Event/Add", CurrentEvent);
            }
        }

        public override ObjectState GetObjectState(Event CurrentEvent)
        {
            return CurrentEvent.Id != default(int) ? ObjectState.Edit : ObjectState.Add;
        }

        public void CurrentObjectToNull(Event CurrentEvent)
        {
            CurrentEvent = null;
        }

        public async Task<List<Event>> GetEvents(User user)
        {
            return await http.GetJsonAsync<List<Event>>("api/Event/GetUserEvents/" + user.Id);
        }

        public string GetObjectName()
        {
            return nameof(Event);
        }
    }
}
