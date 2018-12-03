using BlazorAgenda.Shared;
using BlazorAgenda.Client.Services;
using Microsoft.AspNetCore.Blazor;
using Microsoft.AspNetCore.Blazor.Components;
using Microsoft.AspNetCore.Blazor.RenderTree;
using Microsoft.JSInterop;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorAgenda.Services.Interfaces;
using BlazorAgenda.Shared.Models;

namespace BlazorAgenda.Client.Viewmodels
{
    public class CalendarViewModel : BlazorComponent
    {
        [Inject]
        protected IUserService Service { get; set; }
        [Parameter]
        protected bool Loaded { get; set; }
        [Parameter]
        protected Action<bool> LoadedChanged { get; set; }

        public List<Event> Events { get; set; }
        public List<Color> Colors { get; set; }
        
        private DateTime selectedDate;
        public DateTime SelectedDate
        {
            get { return selectedDate; }
            set
            {
                selectedDate = value;
                GoToSelectedDate();
            }
        }
        public DateTime StartOfWeekDate { get; set; }
        
        public string CurrentMonthAndYear { get; set; }
        
        protected override async Task OnInitAsync()
        {
            //Colors = await Service.GetColors();
            Events = await Service.GetEvents();
            Colors = new List<Color>();
            DragDropHelper.Items = Events;
            //Events = new List<CalendarEvent>();
            GoToCurrentWeek();
        }

        public void GoToPreviousWeek()
        {
            StartOfWeekDate = StartOfWeekDate.AddDays(-7);
            CurrentMonthAndYear = GetCurrentMonthAndYear();
            NotifyLoaded();
        }

        public void GoToCurrentWeek()
        {
            SelectedDate = DateTime.Today;
        }

        public void GoToNextWeek()
        {
            StartOfWeekDate = StartOfWeekDate.AddDays(7);
            CurrentMonthAndYear = GetCurrentMonthAndYear();
            NotifyLoaded();
        }

        public void GoToSelectedDate()
        {
            int delta = DayOfWeek.Monday - SelectedDate.DayOfWeek;
            if (delta > 0)
                delta -= 7;
            StartOfWeekDate = SelectedDate.AddDays(delta);
            CurrentMonthAndYear = GetCurrentMonthAndYear();
            NotifyLoaded();
        }

        public string GetCurrentMonthAndYear()
        {
            string startMonth = StartOfWeekDate.ToString("MMMM");
            string startYear = StartOfWeekDate.ToString("yyyy");
            DateTime endOfWeekDate = StartOfWeekDate.AddDays(6);
            string endMonth = endOfWeekDate.ToString("MMMM");
            string endYear = endOfWeekDate.ToString("yyyy");
            string monthAndYear;
            if (endYear == startYear)
            {
                if (endMonth == startMonth)
                    monthAndYear = startMonth + " " + startYear;
                else
                    monthAndYear = startMonth + " - " + endMonth + " " + startYear;
            }
            else
                monthAndYear = startMonth + " " + startYear + " - " + endMonth + " " + endYear;
            return monthAndYear;
        }

        public void NotifyLoaded()
        {
            Loaded = true;
            LoadedChanged?.Invoke(Loaded);
        }

        public void OnDrop()
        {

        }
    }
}
