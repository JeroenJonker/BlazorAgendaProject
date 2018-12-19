using BlazorAgenda.Client.Services;
using BlazorAgenda.Services.Interfaces;
using BlazorAgenda.Shared.Interfaces;
using BlazorAgenda.Shared.Models;
using Microsoft.AspNetCore.Blazor.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorAgenda.Client.Viewmodels
{
    public class LoginViewmodel : BlazorComponent
    {
        public bool ShowAddUser { get; set; } = false;
        [Parameter] Action<User> OnLogin { get; set; }
        [Inject] protected IUser User { get; set; }
        [Inject] protected IUserService UserService { get; set; }
        [Inject] protected UserViewService UserView {get;set;}

        public async Task LoginAsync()
        {
            if (await UserService.CheckUser((User)User) is User checkedUser)
            {
                OnLogin?.Invoke(checkedUser);
            }
        }

        public void AddUser()
        {
            UserView.ChangeVisibility();
        }

        public void OnCloseDialog()
        {
            ShowAddUser = false;
        }
    }
}
