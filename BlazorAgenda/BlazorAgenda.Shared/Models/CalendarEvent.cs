using BlazorAgenda.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorAgenda.Shared.Models
{
    public partial class CalendarEvent : ICalendarEvent
    {
        public int Id { get; set; }
        public Event Event { get; set; }
        public string Color { get; set; }
    }
}
