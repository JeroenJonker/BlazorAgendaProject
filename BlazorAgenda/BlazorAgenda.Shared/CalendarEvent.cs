using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorAgenda.Shared
{
    public class CalendarEvent : BaseObject
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Summary { get; set; }
        public string ColorId { get; set; }
        public string ICalUID { get; set; }
        public string Location { get; set; }
        public string ID { get; set; }
        public bool Added { get; set; } = false;
    }
}
