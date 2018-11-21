using BlazorAgenda.Shared;
using BlazorAgenda.Services;
using Microsoft.AspNetCore.Blazor;
    using Microsoft.AspNetCore.Blazor.Components;
using Microsoft.AspNetCore.Blazor.RenderTree;
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

        public async Task GetColors()
        {
            Colors = await Service.GetColors();
        }

        public async Task GetEvents()
        {
            Events = await Service.GetEvents();
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
            Head = builder =>
            {
                int seq = 0;
                builder.OpenElement(seq, "tr");
                builder.OpenElement(++seq, "th");
                builder.CloseElement();
                for(int col = start; col < start + 7; col++)
                {
                    DateTime day = StartOfWeekDate.AddDays(col - start);
                    string name = day.ToString("dddd");

                    builder.OpenElement(++seq, "th");
                    builder.AddAttribute(++seq, "onclick", CalendarClickHandler);
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
                    for (int col = 0; col < 7; col++)
                    {
                        DateTime cellStart = cellDateTime.AddDays(col);
                        DateTime cellEnd = cellStart.AddMinutes(15);
                        List<CalendarEvent> activeEvents = Events.FindAll(x => x.Start <= cellStart && x.End >= cellEnd);
                        if (activeEvents.Count > 0)
                        {
                            foreach (CalendarEvent ev in activeEvents)
                            {
                                if (ev.Start == cellStart)
                                {
                                    builder.OpenElement(++seq, "td");
                                    builder.AddAttribute(++seq, "onclick", CalendarClickHandler);
                                    double quarterHours = (ev.End - ev.Start).TotalHours * 4;
                                    builder.AddAttribute(++seq, "rowspan", quarterHours);
                                    builder.OpenElement(++seq, "span");
                                    Color activeColor = Colors.Find(x => x.ColorId == ev.ColorId);
                                    builder.AddAttribute(++seq, "style", "background-color:" + ((activeColor != null) ? activeColor.Background : "#039BE5"));
                                    builder.AddContent(++seq, ev.Summary);
                                    builder.CloseElement();
                                    builder.CloseElement();
                                }
                            }
                        }
                        else
                        {
                            builder.OpenElement(++seq, "td");
                            builder.AddAttribute(++seq, "onclick", CalendarClickHandler);
                            builder.CloseElement();
                        }
                    }
                    builder.CloseElement();
                    for (int row = 0; row < 3; row++)
                    {
                        cellDateTime = cellDateTime.AddMinutes(15);
                        builder.OpenElement(++seq, "tr");
                        for(int innerCol = 0; innerCol < 7; innerCol++)
                        {
                            DateTime cellStart = cellDateTime.AddDays(innerCol);
                            DateTime cellEnd = cellStart.AddMinutes(15);
                            List<CalendarEvent> activeEvents = Events.FindAll(x => x.Start <= cellStart && x.End >= cellEnd);
                            if (activeEvents.Count > 0)
                            {
                                foreach (CalendarEvent ev in activeEvents)
                                {
                                    if (ev.Start == cellStart)
                                    {
                                        builder.OpenElement(++seq, "td");
                                        builder.AddAttribute(++seq, "onclick", CalendarClickHandler);
                                        double quarterHours = (ev.End - ev.Start).TotalHours * 4;
                                        builder.AddAttribute(++seq, "rowspan", quarterHours);
                                        builder.OpenElement(++seq, "span");
                                        Color activeColor = Colors.Find(x => x.ColorId == ev.ColorId);
                                        builder.AddAttribute(++seq, "style", "background-color:" + ((activeColor != null) ? activeColor.Background : "#039BE5"));
                                        builder.AddContent(++seq, ev.Summary);
                                        builder.CloseElement();
                                        builder.CloseElement();
                                    }
                                }
                            }
                            else
                            {
                                builder.OpenElement(++seq, "td");
                                builder.AddAttribute(++seq, "onclick", CalendarClickHandler);
                                builder.CloseElement();
                            }
                        }
                        builder.CloseElement();
                    }
                    cellDateTime = cellDateTime.AddMinutes(15);
                }
            };
            Loaded = true;
            LoadedChanged?.Invoke(Loaded);
        }

        protected void CalendarClickHandler(UIMouseEventArgs e)
        {
            Console.WriteLine("Clicked at {0}, {1}", e.ClientX, e.ClientY);
        }
    }
}
