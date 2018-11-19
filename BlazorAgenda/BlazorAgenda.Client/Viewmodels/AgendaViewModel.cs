using BlazorAgenda.Shared;
using Microsoft.AspNetCore.Blazor;
using Microsoft.AspNetCore.Blazor.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BlazorAgenda.Client.Viewmodels
{
    public class AgendaViewModel : BlazorComponent
    {
        [Inject]
        protected HttpClient HttpClient { get; set; }

        public List<CalendarEvent> Events { get; private set; }
        public DateTime Today { get; private set; }
        public DateTime Monday { get; private set; }
        public event Action OnChange;

        public AgendaViewModel()
        {
        }

        public async Task GetUpcomingEvents()
        {
            Today = DateTime.Today;
            int delta = DayOfWeek.Monday - Today.DayOfWeek;
            if (delta > 0)
                delta -= 7;
            Monday = Today.AddDays(delta);
            Events = await HttpClient.GetJsonAsync<List<CalendarEvent>>("api/SampleData/GetEvents");
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
