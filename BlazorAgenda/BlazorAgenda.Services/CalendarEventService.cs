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
        private List<CalendarEvent> currentCollection;
        private List<Color> colors;
        public List<CalendarEvent> CurrentCollection
        {
            get
            {
                if (currentCollection == null)
                {
                    currentCollection = new List<CalendarEvent>();
                }
                return currentCollection;
            }
            set
            {
                currentCollection = value;
            }
        }

        public List<Color> Colors
        {
            get
            {
                if (colors == null)
                {
                    colors = new List<Color>();
                }
                return colors;
            }
            set
            {
                colors = value;
            }
        }

        public CalendarEventService(HttpClient client)
        {
            http = client;
        }


        public async Task ExecuteAsync()
        {
            Console.WriteLine("Succeeded");
            await http.PostJsonAsync("api/SampleData/PostEvent", CurrentObject);
        }
        public async Task<List<CalendarEvent>> GetCollection()
        {
            CurrentCollection = await http.GetJsonAsync<List<CalendarEvent>>("api/SampleData/GetEvents");
            return CurrentCollection;
        }

        public async Task<List<Color>> GetColors()
        {
            Colors = await http.GetJsonAsync<List<Color>>("api/SampleData/GetColors");
            return Colors;
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
