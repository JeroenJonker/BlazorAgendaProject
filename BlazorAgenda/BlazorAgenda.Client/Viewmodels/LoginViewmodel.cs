﻿using BlazorAgenda.Services.Interfaces;
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
        [Inject] protected IUserService UserService { get; set; }

        public async Task LoginAsync()
        {
            if (await UserService.CheckUser(UserService.CurrentUser) is User checkedUser)
            {
                OnLogin?.Invoke(checkedUser);
            }
        }

        public void AddUser()
        {
            UserService.CurrentUser = new User();
            ShowAddUser = true;
        }

        public void OnCloseDialog()
        {
            UserService.CurrentUser = new User();
            ShowAddUser = false;
        }
    }
}
