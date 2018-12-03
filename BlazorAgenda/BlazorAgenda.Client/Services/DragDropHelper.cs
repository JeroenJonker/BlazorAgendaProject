using BlazorAgenda.Shared.Models;
using System.Collections.Generic;

namespace BlazorAgenda.Client.Services
{
    public static class DragDropHelper
    {
        public static List<Event> Items { get; set; } = new List<Event>();
        public static Event Item { get; set; }
    }
}
