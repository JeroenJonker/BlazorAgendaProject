using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BlazorAgenda.Shared;
using Microsoft.AspNetCore.Blazor;

namespace BlazorAgenda.Services
{
    public class CalendarService
    {
        private List<CalendarEvent> events;
        private List<Color> colors;
        private readonly HttpClient http;

        public List<CalendarEvent> Events
        {
            get
            {
                if (events == null)
                {
                    events = new List<CalendarEvent>();
                }
                return events;
            }
            set
            {
                events = value;
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

        public CalendarService(HttpClient client)
        {
            http = client;
        }

        public async Task<List<CalendarEvent>> GetEvents()
        {
            Events = await http.GetJsonAsync<List<CalendarEvent>>("api/SampleData/GetEvents");
            return Events;
        }

        public async Task<List<Color>> GetColors()
        {
            Colors = await http.GetJsonAsync<List<Color>>("api/SampleData/GetColors");
            return Colors;
        }
    }
}
