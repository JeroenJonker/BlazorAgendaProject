using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BlazorAgenda.Services.Interfaces;
using BlazorAgenda.Shared;
using Microsoft.AspNetCore.Blazor;

namespace BlazorAgenda.Services
{
    public class CalendarEventService : ICalendarEventService
    {
        private readonly HttpClient http;
        public CalendarEvent CurrentObject { get; set; }

        public CalendarEventService(HttpClient client)
        {
            http = client;
        }

        public async Task PostAsync()
        {
            Console.WriteLine("Succeeded");
            await http.PostJsonAsync("api/SampleData/PostEvent", CurrentObject);
        }

        public event Action OnChange;

        public void NotifyStateChanged()
        {
            Console.WriteLine("Changed");
            OnChange?.Invoke();
        }

        public void CurrentObjectToNull()
        {
            CurrentObject = null;
        }
    }
}
