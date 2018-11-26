using BlazorAgenda.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorAgenda.Services.Interfaces
{
    public interface ICalendarEventService : IDefaultObjectService<CalendarEvent>
    {
        CalendarEvent CurrentObject { get; set; }
    }
}
