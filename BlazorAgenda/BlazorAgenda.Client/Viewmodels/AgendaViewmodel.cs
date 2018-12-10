using BlazorAgenda.Client.Views;
using Microsoft.AspNetCore.Blazor.Components;
using System;
using BlazorAgenda.Services.Interfaces;
using Microsoft.AspNetCore.Blazor;
using BlazorAgenda.Shared.Models;

namespace BlazorAgenda.Client.Viewmodels
{
    public class AgendaViewmodel : BlazorComponent
    {
        [Inject]
        protected IStateService StateService { get; set; }
        public bool IsFocus = true;
        public bool IsLoginCompleted = false;
        public bool isLoaded = false;

        public void AddEvent()
        {
            StateService.ObjectFocus = typeof(Event);
        }

        public void EditUser()
        {
            StateService.ObjectFocus = typeof(User);
        }

        public void OnCloseDialog()
        {
            Console.WriteLine("CLose2.5");
            StateService.ObjectFocus = null;
            //StateHasChanged();
        }

        public void OnLoginCompleted(User user)
        {
            StateService.LoginUser = user;
            StateService.ChosenContacts.Add(StateService.LoginUser);
            Console.WriteLine(StateService.ChosenContacts[0].Firstname);
            IsLoginCompleted = true;
            StateHasChanged();
        }

        public void ChildLoadedEvent(bool _isLoaded)
        {
            isLoaded = _isLoaded;
            StateHasChanged();
        }
    }
}
