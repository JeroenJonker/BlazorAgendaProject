using BlazorAgenda.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorAgenda.Shared.Interfaces
{
    public interface IUser : IBaseObject
    {
        string Emailadress { get; set; }
        string Firstname { get; set; }
        string Lastname { get; set; }
        string Password { get; set; }
        bool Isadmin { get; set; }

        ICollection<Event> Event { get; set; }
    }
}
