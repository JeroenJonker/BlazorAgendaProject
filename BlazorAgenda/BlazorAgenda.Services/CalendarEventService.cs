using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BlazorAgenda.Shared;
using Microsoft.AspNetCore.Blazor;

namespace BlazorAgenda.Services
{
    public class CalendarEventService : IDefaultObjectService
    {
        private readonly HttpClient http;
        public CalendarEvent CurrentObject { get; set; }

        public CalendarEventService(HttpClient client)
        {
            http = client;
        }

        public async Task PostAsync()
        {
            await http.PostJsonAsync("api/SampleData/PostEvent", CurrentObject);
        }

        public void ChangeCurrentObject(CalendarEvent calendarEvent)
        {
            CurrentObject = calendarEvent;
            NotifyStateChanged();
            Console.WriteLine("Changed");
        }

        public event Action OnChange;

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
