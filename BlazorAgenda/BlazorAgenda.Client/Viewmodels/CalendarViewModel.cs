using BlazorAgenda.Shared;
using BlazorAgenda.Services;
using BlazorAgenda.Client.Viewmodels.BaseViewModels;
using Microsoft.AspNetCore.Blazor.Components;
using Microsoft.AspNetCore.Blazor;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Blazor.RenderTree;

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
        public DateTime Today { get; set; }
        public DateTime StartOfWeekDate { get; set; }

        public RenderFragment CurrentMonthAndYear { get; set; }
        public RenderFragment Head { get; set; }
        public RenderFragment Body { get; set; }

        public async Task GetAsync()
        {
            Events = await Service.GetAsync();
        }
        
        public void GoToPreviousWeek()
        {
            StartOfWeekDate = StartOfWeekDate.AddDays(-7);
            GetCurrentMonthAndYear();
            GetCalendar();
        }

        public void GoToCurrentWeek()
        {
            Today = DateTime.Today;
            int delta = DayOfWeek.Monday - Today.DayOfWeek;
            if (delta > 0)
                delta -= 7;
            StartOfWeekDate = Today.AddDays(delta);
            GetCurrentMonthAndYear();
            GetCalendar();
        }

        public void GoToNextWeek()
        {
            StartOfWeekDate = StartOfWeekDate.AddDays(7);
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
                    string columnClass = (day == Today) ? "day active" : "day";
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
                        builder.OpenElement(++seq, "td");
                        builder.CloseElement();
                    }
                    builder.CloseElement();
                    for (int row = 0; row < 3; row++)
                    {
                        builder.OpenElement(++seq, "tr");
                        for(int innerCol = 0; innerCol < 7; innerCol++)
                        {
                            builder.OpenElement(++seq, "td");
                            builder.CloseElement();
                        }
                        builder.CloseElement();
                    }
                }
            };
            Loaded = true;
            LoadedChanged?.Invoke(Loaded);
        }
    }
}
