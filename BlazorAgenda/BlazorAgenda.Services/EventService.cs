using BlazorAgenda.Services.Interfaces;
using BlazorAgenda.Shared.Models;
using Microsoft.AspNetCore.Blazor;
using System;
using System.Collections.Generic;
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
            //CurrentEvent = new Event
            //{
            //    Start = DateTime.Now,
            //    Emailadress = state.CurrentUser.Emailadress,
            //    //EmailadressNavigation = UserService.CurrentUser,
            //    End = DateTime.Now
            //};
        }

        public async Task ExecuteAsync()
        {
            if (CurrentEvent.Id != default(int))
            {
                await http.PutJsonAsync("api/Event/Edit", CurrentEvent);
            }
            else
            {
                await http.PostJsonAsync("api/Event/Add", CurrentEvent);
            }
        }

        public void CurrentObjectToNull()
        {
            CurrentEvent = null;
        }
    }
}
