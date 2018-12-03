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
    public class EventService : IEventService
    {
        private readonly HttpClient http;

        public event Action OnChange;

        public Event CurrentEvent { get; set; }

        public EventService(HttpClient http)
        {
            this.http = http;
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

        public void NotifyStateChanged()
        {
            OnChange?.Invoke();
        }

        public void CurrentObjectToNull()
        {
            CurrentEvent = null;
        }
    }
}
