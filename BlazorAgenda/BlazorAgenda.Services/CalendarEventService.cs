using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BlazorAgenda.Shared;
using Microsoft.AspNetCore.Blazor;

namespace BlazorAgenda.Services
{
    public class CalendarEventService : IDefaultObjectService<CalendarEvent>
    {
        private readonly HttpClient http;
        public CalendarEvent CurrentObject { get; set; }

        public CalendarEventService(HttpClient client)
        {
            http = client;
        }

        public async Task ExecuteAsync()
        {
            await http.PostJsonAsync("api/SampleData/PostEvent", CurrentObject);
        }

        public event Action OnChange;

        public void NotifyStateChanged()
        {
            Console.WriteLine("Changed");
            OnChange?.Invoke();
        }
    }
}
