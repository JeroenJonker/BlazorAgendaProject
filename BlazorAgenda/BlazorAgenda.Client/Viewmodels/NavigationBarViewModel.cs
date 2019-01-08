using BlazorAgenda.Client.Services;
using BlazorAgenda.Services.Interfaces;
using Microsoft.AspNetCore.Blazor.Components;

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
            StateService.CurrentPage = BlazorAgenda.Services.Pages.Users;
            StateService.NotifyStateChanged();
        }

        public void ViewAgenda()
        {
            StateService.CurrentPage = BlazorAgenda.Services.Pages.Agenda;
            StateService.NotifyStateChanged();
        }
    }
}
