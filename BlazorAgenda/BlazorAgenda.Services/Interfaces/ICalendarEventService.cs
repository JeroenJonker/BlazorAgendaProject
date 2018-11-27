using BlazorAgenda.Shared;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAgenda.Services.Interfaces
{
    public interface ICalendarEventService : IDefaultObjectService
    {
        CalendarEvent CurrentObject { get; set; }
        List<CalendarEvent> CurrentCollection { get; set; }
        Task<List<CalendarEvent>> GetCollection();
        Task<List<Color>> GetColors();
    }
}
