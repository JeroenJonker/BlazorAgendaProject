using BlazorAgenda.Shared.Enums;
using BlazorAgenda.Shared.Models;
using System;
using System.Collections.Generic;

namespace BlazorAgenda.Services.Interfaces
{
    public interface IStateService
    {
        User LoginUser { get; set; }
        List<User> ChosenContacts { get; set; }
        void ResetState();
        event Action OnChange;
        Pages CurrentPage { get; set; }
        void NotifyStateChanged();
    }
}
