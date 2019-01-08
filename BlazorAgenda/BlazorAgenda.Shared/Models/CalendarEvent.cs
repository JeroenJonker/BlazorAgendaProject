using BlazorAgenda.Shared.Interfaces;

namespace BlazorAgenda.Shared.Models
{
    public partial class CalendarEvent : ICalendarEvent
    {
        public int Id { get; set; }
        public Event Event { get; set; }
        public string Color { get; set; }
    }
}
