using BlazorAgenda.Client.Viewmodels.BaseViewModels;
using BlazorAgenda.Services.Interfaces;
using BlazorAgenda.Shared.Models;
using Microsoft.AspNetCore.Blazor.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorAgenda.Client.Viewmodels
{
    public class UserViewModel : ObjectBase
    {
        [Inject] protected IUserService Service { get; set; }
        [Parameter] protected User SetUser { get; set; }

        protected override void OnInit()
        {
            base.OnInit();
            if (SetUser != null)
            {
                Service.CurrentUser = SetUser;
                Service.CurrentUser.Password = "";
            }
        }

        public void ConvertPassword()
        {
            Service.CurrentUser.Password = Service.ConvertStringToHash(Service.CurrentUser.Password);
        }
    }
}
