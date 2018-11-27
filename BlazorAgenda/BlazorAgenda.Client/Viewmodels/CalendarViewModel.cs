using BlazorAgenda.Client.Views;
using BlazorAgenda.Shared;
using BlazorAgenda.Services;
using Microsoft.AspNetCore.Blazor;
using Microsoft.AspNetCore.Blazor.Components;
using Microsoft.AspNetCore.Blazor.RenderTree;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorAgenda.Client.Viewmodels
{
    public class CalendarViewModel : BlazorComponent
    {
        [Inject]
        protected CalendarService Service { get; set; }
        [Parameter]
        protected bool Loaded { get; set; }
        [Parameter]
        protected Action<bool> LoadedChanged { get; set; }

        public List<CalendarEvent> Events { get; set; }
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
        
        public RenderFragment CurrentMonthAndYear { get; set; }
        public RenderFragment Head { get; set; }
        public RenderFragment Body { get; set; }

        protected override async Task OnInitAsync()
        {
            Colors = await Service.GetColors();
            Events = await Service.GetEvents();
            GoToCurrentWeek();
        }
                
        public void GoToPreviousWeek()
        {
            StartOfWeekDate = StartOfWeekDate.AddDays(-7);
            GetCurrentMonthAndYear();
            GetCalendar();
        }

        public void GoToCurrentWeek()
        {
            SelectedDate = DateTime.Today;
        }

        public void GoToNextWeek()
        {
            StartOfWeekDate = StartOfWeekDate.AddDays(7);
            GetCurrentMonthAndYear();
            GetCalendar();
        }

        public void GoToSelectedDate()
        {
            int delta = DayOfWeek.Monday - SelectedDate.DayOfWeek;
            if (delta > 0)
                delta -= 7;
            StartOfWeekDate = SelectedDate.AddDays(delta);
            GetCurrentMonthAndYear();
            GetCalendar();
        }

        public void GetCurrentMonthAndYear()
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
            CurrentMonthAndYear = builder =>
            {
                builder.OpenElement(0, "h2");
                builder.AddContent(1, monthAndYear);
                builder.CloseElement();
            };
        }

        public void GetCalendar()
        {
            int start = StartOfWeekDate.Day;
            GetHead(start);
            GetBody();
            Loaded = true;
            LoadedChanged?.Invoke(Loaded);
        }

        private void GetHead(int start)
        {
            Head = builder =>
            {
                int seq = 0;
                builder.OpenElement(seq, "tr");
                builder.OpenElement(++seq, "th");
                builder.CloseElement();
                for (int col = start; col < start + 7; col++)
                {
                    DateTime day = StartOfWeekDate.AddDays(col - start);
                    string name = day.ToString("dddd");

                    builder.OpenElement(++seq, "th");
                    string columnClass = (day == SelectedDate) ? "day active" : "day";
                    builder.OpenElement(++seq, "span");
                    builder.AddAttribute(++seq, "class", columnClass);
                    builder.AddContent(++seq, day.Day);
                    builder.CloseElement();
                    builder.OpenElement(++seq, "span");
                    builder.AddAttribute(++seq, "class", "long");
                    builder.AddContent(++seq, name);
                    builder.CloseElement();
                    builder.OpenElement(++seq, "span");
                    builder.AddAttribute(++seq, "class", "short");
                    builder.AddContent(++seq, name.Substring(0, 3));
                    builder.CloseElement();
                    builder.CloseElement();
                }
                builder.CloseElement();
            };
        }

        private void GetBody()
        {
            Body = builder =>
            {
                int seq = 0;
                DateTime cellDateTime = StartOfWeekDate;
                for (int hour = 0; hour < 24; hour++)
                {
                    builder.OpenElement(++seq, "tr");
                    builder.OpenElement(++seq, "td");
                    builder.AddAttribute(++seq, "class", "hour");
                    builder.AddAttribute(++seq, "rowspan", "4");
                    builder.OpenElement(++seq, "span");
                    builder.AddContent(++seq, hour.ToString() + ":00");
                    builder.CloseElement();
                    builder.CloseElement();
                    seq = GenerateWeekColumns(builder, seq, cellDateTime);
                    builder.CloseElement();
                    for (int row = 0; row < 3; row++)
                    {
                        cellDateTime = cellDateTime.AddMinutes(15);
                        builder.OpenElement(++seq, "tr");
                        seq = GenerateWeekColumns(builder, seq, cellDateTime);
                        builder.CloseElement();
                    }
                    cellDateTime = cellDateTime.AddMinutes(15);
                }
            };
        }

        private int GenerateWeekColumns(RenderTreeBuilder builder, int seq, DateTime cellDateTime)
        {
            for (int col = 0; col < 7; col++)
            {
                DateTime cellStart = cellDateTime.AddDays(col);
                DateTime cellEnd = cellStart.AddMinutes(15);
                List<CalendarEvent> startEvents = Events.FindAll(x => x.Start == cellStart && !x.Added);
                if (startEvents.Count > 0)
                {
                    builder.OpenElement(++seq, "td");
                    CalendarEvent first = startEvents.OrderByDescending(x => (x.End - x.Start).TotalHours).First();
                    List<CalendarEvent> otherEvents = Events.FindAll(x => x.Start > first.Start && x.Start < first.End);
                    double quarterHours;
                    if (otherEvents.Count > 0)
                    {
                        CalendarEvent last = otherEvents.OrderByDescending(x => (x.End - first.Start).TotalHours).First();
                        quarterHours = (last.End - first.Start).TotalHours * 4;
                    }
                    else
                        quarterHours = (first.End - first.Start).TotalHours * 4;
                    builder.AddAttribute(++seq, "rowspan", quarterHours);
                    builder.OpenElement(++seq, "table");
                    builder.OpenElement(++seq, "tbody");
                    builder.OpenElement(++seq, "tr");
                    foreach (CalendarEvent startEvent in startEvents)
                    {
                        seq = AddEventComponent(builder, seq, startEvent);
                    }
                    foreach (CalendarEvent oev in otherEvents)
                    {
                        builder.OpenElement(++seq, "td");
                        builder.CloseElement();
                    }
                    builder.CloseElement();
                    DateTime eventTime = cellStart.AddMinutes(15);
                    for (int i = 1; i < quarterHours; i++)
                    {
                        builder.OpenElement(++seq, "tr");
                        int finishedStartEvents = startEvents.FindAll(x => x.End <= eventTime).Count;
                        int finishedOtherEvents = otherEvents.FindAll(y => y.End <= eventTime).Count;
                        List<CalendarEvent> rowStartEvents = otherEvents.FindAll(x => x.Start == eventTime);
                        if (rowStartEvents.Count > 0)
                        {
                            foreach (CalendarEvent rowEvent in rowStartEvents)
                            {
                                rowEvent.Added = true;
                                seq = AddEventComponent(builder, seq, rowEvent);
                            }
                        }
                        int addedEvents = otherEvents.FindAll(x => x.Added == true).Count;
                        int emptyColumns = otherEvents.Count - addedEvents + finishedStartEvents + finishedOtherEvents;
                        for (int j = 0; j < emptyColumns; j++)
                        {
                            builder.OpenElement(++seq, "td");
                            builder.CloseElement();
                        }
                        builder.CloseElement();
                        eventTime = eventTime.AddMinutes(15);
                    }
                    builder.CloseElement();
                    builder.CloseElement();
                    builder.CloseElement();
                }
                else if (Events.Find(x => x.Start < cellStart && x.End >= cellEnd) == null)
                {
                    builder.OpenElement(++seq, "td");
                    builder.CloseElement();
                }
            }
            return seq;
        }

        private int AddEventComponent(RenderTreeBuilder builder, int seq, CalendarEvent ev)
        {
            builder.OpenElement(++seq, "td");
            builder.AddAttribute(++seq, "rowspan", (ev.End - ev.Start).TotalHours * 4);
            Color color = Colors.Find(x => x.ColorId == ev.ColorId);
            builder.OpenComponent<EventView>(++seq);
            builder.AddAttribute(++seq, "EventColor", color);
            builder.AddAttribute(++seq, "Event", ev);
            builder.CloseComponent();
            builder.CloseElement();
            return seq;
        }
    }
}
