using BlazorAgenda.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorAgenda.Shared.Interfaces
{
    public interface IEvent : IBaseObject
    {
        string Summary { get; set; }
        DateTime Start { get; set; }
        DateTime End { get; set; }
        string Location { get; set; }
        int Userid { get; set; }

        User User { get; set; }
    }
}
