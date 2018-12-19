using BlazorAgenda.Client.Services;
using BlazorAgenda.Services.Interfaces;
using BlazorAgenda.Shared.Models;
using Microsoft.AspNetCore.Blazor.Components;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BlazorAgenda.Client.Viewmodels
{
    public class LoginViewmodel : BlazorComponent
    {
        public bool ShowAddUser { get; set; } = false;
        [Parameter] Action<User> OnLogin { get; set; }
        [Inject] protected User User { get; set; }
        [Inject] protected IUserService UserService { get; set; }
        [Inject] protected UserViewService UserView {get;set;}

        public string Style { get; set; }

        public async void LoginAsync()
        {
            Regex r = new Regex(@"^(([^<>()[\]\\.,;:\s@\""]+(\.[^<>()[\]\\.,;:\s@\""]+)*)|(\"".+\""))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$");
            if (User.Emailadress != null && User.Password != null && r.IsMatch(User.Emailadress))
            {
                if (await UserService.CheckUser(User) is User checkedUser)
                {
                    Style = "";
                    OnLogin?.Invoke(checkedUser);
                    return;
                }
                else
                {
                    User.Password = null;
                }
            }
            Style = "border-color: red;";
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
