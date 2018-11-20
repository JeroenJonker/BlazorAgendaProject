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
        private List<CalendarEvent> currentObject;
        private readonly HttpClient http;

        public List<CalendarEvent> CurrentObject
        {
            get
            {
                if (currentObject == null)
                {
                    currentObject = new List<CalendarEvent>();
                }
                return currentObject;
            }
            set
            {
                currentObject = value;
            }
        }

        public CalendarService(HttpClient client)
        {
            http = client;
        }

        public async Task<List<CalendarEvent>> GetAsync()
        {
            CurrentObject = await http.GetJsonAsync<List<CalendarEvent>>("api/SampleData/GetEvents");
            return CurrentObject;
        }
    }
}
