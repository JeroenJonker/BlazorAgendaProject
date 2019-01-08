using BlazorAgenda.Services.Interfaces;
using BlazorAgenda.Shared.Interfaces;
using BlazorAgenda.Shared.Models;
using Microsoft.AspNetCore.Blazor.Components;
using System;
using System.Text.RegularExpressions;

namespace BlazorAgenda.Client.Viewmodels
{
    public class LoginViewmodel : BlazorComponent
    {
        [Parameter] Action<User> OnLogin { get; set; }
        [Inject] protected IUser User { get; set; }
        [Inject] protected IUserService UserService { get; set; }

        public string Style { get; set; }
        public bool IsLoggingIn { get; set; } = false;

        public async void LoginAsync()
        {
            IsLoggingIn = true;
            Regex r = new Regex(@"^(([^<>()[\]\\.,;:\s@\""]+(\.[^<>()[\]\\.,;:\s@\""]+)*)|(\"".+\""))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$");
            if (User.Emailadress != null && User.Password != null && r.IsMatch(User.Emailadress))
            {
                if (await UserService.CheckUser(User as User) is User checkedUser)
                {
                    Style = "";
                    OnLogin?.Invoke(checkedUser);
                }
                else
                {
                    User.Password = null;
                    Style = "border-color: red;";
                    StateHasChanged();
                }
            }
            else
            {
                Style = "border-color: red;";
            }
            IsLoggingIn = false;
        }
    }
}
