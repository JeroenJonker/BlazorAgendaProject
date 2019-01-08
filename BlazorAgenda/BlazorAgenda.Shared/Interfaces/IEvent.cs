using BlazorAgenda.Shared.Interfaces.BaseObjects;
using BlazorAgenda.Shared.Models;
using System;

namespace BlazorAgenda.Shared.Interfaces
{
    public interface IEvent : IBaseObject
    {
        string Summary { get; set; }
        DateTime Start { get; set; }
        DateTime End { get; set; }
        string Location { get; set; }
        int Userid { get; set; }
        bool Isprivate { get; set; }

        User User { get; set; }
    }
}
