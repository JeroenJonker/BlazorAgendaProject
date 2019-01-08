using BlazorAgenda.Shared.Interfaces.BaseObjects;
using BlazorAgenda.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorAgenda.Shared.Interfaces
{
    public interface ICalendarEvent : IBaseObject
    {
        Event Event { get; set; }
        string Color { get; set; }
    }
}
