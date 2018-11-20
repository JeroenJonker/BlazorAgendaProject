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
        private CalendarEvent currentobject;
        private readonly HttpClient http;

        public CalendarEvent CurrentObject
        {
            get
            {
                if (currentobject == null)
                {
                    currentobject = new CalendarEvent
                    {
                        Start = DateTime.Now,
                        End = DateTime.Now
                    };
                }
                return currentobject;
            }
            set
            {
                CurrentObject = value;
            }
        }

        public CalendarEventService(HttpClient client)
        {
            http = client;
        }

        public async Task PostAsync()
        {
            await http.PostJsonAsync("api/SampleData/PostEvent", CurrentObject);
        }
    }
}
