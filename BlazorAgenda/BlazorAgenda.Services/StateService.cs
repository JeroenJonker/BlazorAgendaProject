using BlazorAgenda.Services.Interfaces;
using BlazorAgenda.Shared;
using BlazorAgenda.Shared.Models;
using Microsoft.AspNetCore.Blazor.Components;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAgenda.Services
{
    public class StateService : IStateService
    {
        [Inject] public User LoginUser { get; set; }

        public List<User> ChosenContacts { get; set; }

        public Type ObjectFocus { get; set; }

        public event Action OnChange;

        public StateService()
        {
            ChosenContacts = new List<User>();
        }

        public void ResetState()
        {
            LoginUser = default(User);
            ChosenContacts.Clear();
            ObjectFocus = null;
        }

        public void NotifyStateChanged() => OnChange?.Invoke();
    }
}
