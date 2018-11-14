using Microsoft.AspNetCore.Blazor.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Google.Apis.Calendar.v3.Data;

namespace BlazorAgenda.Client.Viewmodels
{
    public class CalendarEventViewmodel : BlazorComponent
    {
        [Inject]
        protected HttpClient HttpClient { get; set; }
        public string Summary { get; set; }
        public DateTime BeginTime { get; set; }
        public DateTime EndTime { get; set; }

        public CalendarEventViewmodel()
        {
            BeginTime = DateTime.Now;
            EndTime = DateTime.Now;
        }
    }
}
