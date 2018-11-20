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
        public DateTime CurrentDate { get; set; } = DateTime.Today;
        public DateTime StartOfWeekDate { get; set; }

        public RenderFragment Head { get; set; }
        public RenderFragment Body { get; set; }

        public async Task GetAsync()
        {
            Events = await Service.GetAsync();
        }

        public void BuildTable()
        {
            int delta = DayOfWeek.Monday - CurrentDate.DayOfWeek;
            if (delta > 0)
                delta -= 7;
            StartOfWeekDate = CurrentDate.AddDays(delta);
            int today = CurrentDate.Day;
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
                    string columnClass = (col == today) ? "day active" : "day";
                    builder.OpenElement(++seq, "span");
                    builder.AddAttribute(++seq, "class", columnClass);
                    builder.AddContent(++seq, col.ToString());
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
