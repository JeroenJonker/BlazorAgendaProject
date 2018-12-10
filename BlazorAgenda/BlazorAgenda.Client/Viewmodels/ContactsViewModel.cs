using BlazorAgenda.Shared;
using BlazorAgenda.Services;
using Microsoft.AspNetCore.Blazor;
using Microsoft.AspNetCore.Blazor.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorAgenda.Services.Interfaces;
using BlazorAgenda.Shared.Models;

namespace BlazorAgenda.Client.Viewmodels
{
    public class ContactsViewModel : BlazorComponent
    {
        [Inject]
        public IUserService UserService { get; set; }
        [Inject]
        public IStateService StateService { get; set; }
        public List<User> Contacts { get; set; }

        protected override async Task OnInitAsync()
        {
            Contacts = await UserService.GetContacts();
        }

        public void ClickContact(User user)
        {
            if (StateService.ChosenContacts.Contains(user))
            {
                StateService.ChosenContacts.Remove(user);
            }
            else
            {
                StateService.ChosenContacts.Add(user);
            }
        }
    }
}
