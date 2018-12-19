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
using BlazorAgenda.Shared.Interfaces;

namespace BlazorAgenda.Client.Viewmodels
{
    public class CalendarViewModel : BlazorComponent
    {
        [Inject]
        protected EventViewService EventViewService { get; set; }
        [Inject]
        protected IEventService EventService { get; set; }
        [Inject]
        protected IStateService StateService { get; set; }
        [Inject] 
        protected IEvent CurrentObject { get; set; }
        
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

        private static readonly string[] colors = new string[]
        {
            "#a4bdfc", "#7ae7bf", "#dbadff", "#ff887c", "#fbd75b", "#ffb878", "#46d6db", "#e1e1e1", "#5484ed", "#51b749", "#dc2127"
        };
        
        protected override async Task OnInitAsync()
        {
            EventViewService.OnClose = CloseEventView;
            await GetEvents();
            GoToCurrentWeek();
        }

        public async Task GetEvents()
        {
            List<Event> events = new List<Event>();
            for (int i = 0; i < StateService.ChosenContacts.Count; i++)
            {
                List<Event> userEvents = await EventService.GetEvents(StateService.ChosenContacts[i]);
                foreach (Event ev in userEvents)
                {
                    ev.Color = colors[i % colors.Length];
                    events.Add(ev);
                }
            }
            DragDropHelper.Items = events.OrderBy(x => x.Start).ToList();
            StateHasChanged();
        }

        public void GoToPreviousWeek()
        {
            StartOfWeekDate = StartOfWeekDate.AddDays(-7);
            CurrentMonthAndYear = GetCurrentMonthAndYear();
            StateHasChanged();
        }

        public void GoToCurrentWeek()
        {
            SelectedDate = DateTime.Today;
        }

        public void GoToNextWeek()
        {
            StartOfWeekDate = StartOfWeekDate.AddDays(7);
            CurrentMonthAndYear = GetCurrentMonthAndYear();
            StateHasChanged();
        }

        public void GoToSelectedDate()
        {
            int delta = DayOfWeek.Monday - SelectedDate.DayOfWeek;
            if (delta > 0)
                delta -= 7;
            StartOfWeekDate = SelectedDate.AddDays(delta);
            CurrentMonthAndYear = GetCurrentMonthAndYear();
            StateHasChanged();
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

        public void OnMoveEvent(Event ev)
        {
            EventService.ExecuteAsync(ev);
            StateHasChanged();
        }

        public void OnNewEvent(DateTime start)
        {
            CurrentObject.Start = start;
            CurrentObject.End = start.AddHours(1);
            CurrentObject.Userid = StateService.LoginUser.Id;
            EventViewService.CurrentObject = CurrentObject as Event;
            EventViewService.ChangeVisibility();
        }

        public void CloseEventView()
        {
            StateHasChanged();
        }
    }
}
