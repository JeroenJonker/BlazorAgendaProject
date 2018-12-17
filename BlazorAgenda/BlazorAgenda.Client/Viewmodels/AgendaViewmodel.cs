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

        protected override void OnInit()
        {
            base.OnInit();
            StateService.OnChange += StateHasChanged;
        }

        public void EditUser()
        {
            StateService.ObjectFocus = typeof(User);
        }

        public void OnLoginCompleted(User user)
        {
            StateService.LoginUser = user;
            StateService.ChosenContacts.Add(user);
            StateHasChanged();
        }
    }
}
