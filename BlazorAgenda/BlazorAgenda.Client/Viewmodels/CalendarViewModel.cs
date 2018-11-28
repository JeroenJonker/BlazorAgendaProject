using BlazorAgenda.Client.Views;
using BlazorAgenda.Shared;
using BlazorAgenda.Services;
using Microsoft.AspNetCore.Blazor;
using Microsoft.AspNetCore.Blazor.Components;
using Microsoft.AspNetCore.Blazor.RenderTree;
using Microsoft.JSInterop;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorAgenda.Services.Interfaces;

namespace BlazorAgenda.Client.Viewmodels
{
    public class CalendarViewModel : BlazorComponent
    {
        [Inject]
        protected ICalendarEventService Service { get; set; }
        [Parameter]
        protected bool Loaded { get; set; }
        [Parameter]
        protected Action<bool> LoadedChanged { get; set; }

        public List<CalendarEvent> Events { get; set; }
        public List<Color> Colors { get; set; }

        public double OffsetTop { get; set; }
        public double OffsetLeft { get; set; }
        public double ColumnWidth { get; set; }
        public double RowHeight { get; set; }

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
            Colors = await Service.GetColors();
            Events = await Service.GetCollection();
            //Colors = new List<Color>();
            //Events = new List<CalendarEvent>();
            GoToCurrentWeek();
            OffsetTop = await JSRuntime.Current.InvokeAsync<double>("interopFunctions.getOffsetTop");
            OffsetLeft = await JSRuntime.Current.InvokeAsync<double>("interopFunctions.getOffsetLeft");
            ColumnWidth = OffsetLeft - 1;
            RowHeight = 16.8d;
        }

        public void GoToPreviousWeek()
        {
            StartOfWeekDate = StartOfWeekDate.AddDays(-7);
            CurrentMonthAndYear = GetCurrentMonthAndYear();
            GetCalendar();
        }

        public void GoToCurrentWeek()
        {
            SelectedDate = DateTime.Today;
        }

        public void GoToNextWeek()
        {
            StartOfWeekDate = StartOfWeekDate.AddDays(7);
            CurrentMonthAndYear = GetCurrentMonthAndYear();
            GetCalendar();
        }

        public void GoToSelectedDate()
        {
            int delta = DayOfWeek.Monday - SelectedDate.DayOfWeek;
            if (delta > 0)
                delta -= 7;
            StartOfWeekDate = SelectedDate.AddDays(delta);
            CurrentMonthAndYear = GetCurrentMonthAndYear();
            GetCalendar();
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

        public void GetCalendar()
        {
            Loaded = true;
            LoadedChanged?.Invoke(Loaded);
        }
    }
}
