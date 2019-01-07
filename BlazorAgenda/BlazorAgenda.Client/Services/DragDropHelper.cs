using BlazorAgenda.Shared.Models;
using System.Collections.Generic;

namespace BlazorAgenda.Client.Services
{
    public static class DragDropHelper
    {
        public static List<CalendarEvent> Items { get; set; } = new List<CalendarEvent>();
        public static CalendarEvent Item { get; set; }
    }
}
