using BlazorAgenda.Services.Interfaces;
using BlazorAgenda.Shared.Models;
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

        public void EditUser()
        {
            StateService.ObjectFocus = typeof(User);
        }

        public void OnCloseDialog()
        {
            StateService.ObjectFocus = null;
        }

        public void Logout()
        {
            StateService.ResetState();
            StateService.NotifyStateChanged();
        }
    }
}
