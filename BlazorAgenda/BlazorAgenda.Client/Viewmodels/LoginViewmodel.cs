using BlazorAgenda.Services.Interfaces;
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
        [Inject] protected User User { get; set; } 
        [Inject] protected IUserService UserService { get; set; }

        public async Task LoginAsync()
        {
            if (await UserService.CheckUser(User) is User checkedUser)
            {
                OnLogin?.Invoke(checkedUser);
            }
        }

        public void AddUser()
        {
            ShowAddUser = true;
        }

        public void OnCloseDialog()
        {
            ShowAddUser = false;
        }
    }
}
