using BlazorAgenda.Shared;
using BlazorAgenda.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorAgenda.Services.Interfaces
{
    public interface IStateService
    {
        User LoginUser { get; set; }
        List<User> ChosenContacts { get; set; }
        Type ObjectFocus { get; set; }
        void ResetState();
        event Action OnChange;
        void NotifyStateChanged();
    }
}
