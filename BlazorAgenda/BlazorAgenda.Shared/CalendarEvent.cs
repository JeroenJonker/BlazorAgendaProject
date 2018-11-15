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
    }
}
