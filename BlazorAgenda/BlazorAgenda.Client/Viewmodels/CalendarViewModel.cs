using BlazorAgenda.Shared;
using BlazorAgenda.Services;
using BlazorAgenda.Client.Viewmodels.BaseViewModels;
using Microsoft.AspNetCore.Blazor.Components;
using Microsoft.AspNetCore.Blazor;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorAgenda.Client.Viewmodels
{
    public class CalendarViewModel : BlazorComponent
    {
        [Inject]
        protected CalendarService Service { get; set; }
        [Parameter]
        private bool Loaded { get; set; }
        [Parameter]
        protected Action<bool> LoadedChanged { get; set; }

        public List<CalendarEvent> Events { get; set; }
        public DateTime CurrentDate { get; set; }
        public DateTime StartOfWeekDate { get; set; }

        public async Task GetAsync()
        {
            Events = await Service.GetAsync();
            CurrentDate = DateTime.Today;
            int delta = DayOfWeek.Monday - CurrentDate.DayOfWeek;
            if (delta > 0)
                delta -= 7;
            StartOfWeekDate = CurrentDate.AddDays(delta);
            Loaded = true;
            LoadedChanged?.Invoke(Loaded);
        }
    }
}
