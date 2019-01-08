using BlazorAgenda.Client.Services;
using BlazorAgenda.Services.Interfaces;
using BlazorAgenda.Shared.Models;
using Microsoft.AspNetCore.Blazor.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorAgenda.Client.Viewmodels
{
    public class UsersViewModel : BlazorComponent
    {
        public List<User> Users { get; set; }

        [Inject] IUserService UserService { get; set; }

        [Inject] protected UserViewService UserView { get; set; }

        protected override async Task OnInitAsync()
        {
            Users = new List<User>();
            UserView.OnSavedChange = CloseUserView;
            Users = await UserService.GetContacts();
        }

        public void EditUser(User user)
        {
            UserView.CurrentObject = user;
            UserView.ChangeVisibility();
        }
        public void AddUser()
        {
            UserView.ChangeVisibility();
        }

        public async Task CloseUserView()
        {
            Users = await UserService.GetContacts();
            StateHasChanged();
        }
    }
}
