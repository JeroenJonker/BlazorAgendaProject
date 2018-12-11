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
    public class EventService : DefaultObjectService, IEventService
    {
        public Event CurrentEvent { get; set; }
        public EventService(HttpClient client, Event currentEvent) : base(client)
        {
            CurrentEvent = currentEvent;
        }

        public async Task ExecuteAsync()
        {
            if (GetObjectState() == ObjectState.Edit)
            {
                await http.PutJsonAsync("api/Event/Edit", CurrentEvent);
            }
            else
            {
                await http.PostJsonAsync("api/Event/Add", CurrentEvent);
            }
        }

        public override ObjectState GetObjectState()
        {
            return CurrentEvent.Id != default(int) ? ObjectState.Edit : ObjectState.Add;
        }

        public void CurrentObjectToNull()
        {
            CurrentEvent = null;
        }

        public async Task<List<Event>> GetEvents(User user)
        {
            return await http.GetJsonAsync<List<Event>>("api/Event/GetUserEvents/" + user.Emailadress);
        }

        public string GetObjectName()
        {
            return nameof(Event);
        }
    }
}
