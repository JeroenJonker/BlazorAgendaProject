using BlazorAgenda.Client.Services;
using BlazorAgenda.Services.Interfaces;
using BlazorAgenda.Shared.Enums;
using Microsoft.AspNetCore.Blazor.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorAgenda.Client.Viewmodels
{
    public class NavigationBarViewModel : BlazorComponent
    {
        [Inject] protected IStateService StateService { get; set; }
        [Inject] protected UserViewService UserView { get; set; }

        public void EditUser()
        {
            UserView.CurrentObject = StateService.LoginUser;
            UserView.ChangeVisibility();
        }

        public void Logout()
        {
            StateService.ResetState();
            StateService.NotifyStateChanged();
        }

        public void ViewUsers()
        {
            StateService.CurrentPage = BlazorAgenda.Shared.Enums.Pages.Users;
            StateService.NotifyStateChanged();
        }

        public void ViewAgenda()
        {
            StateService.CurrentPage = BlazorAgenda.Shared.Enums.Pages.Agenda;
            StateService.NotifyStateChanged();
        }
    }
}
