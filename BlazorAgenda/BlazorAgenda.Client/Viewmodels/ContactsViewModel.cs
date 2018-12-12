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

        [Parameter]
        protected Action OnUpdate { get; set; }

        public List<User> Contacts { get; set; }

        protected override async Task OnInitAsync()
        {
            Contacts = await UserService.GetContacts();
        }

        public void SelectContact(UIChangeEventArgs e)
        {
            User user = StateService.ChosenContacts.Find(x => x.Id == Convert.ToInt32(e.Value));
            if (user == null)
            {
                user = Contacts.Find(x => x.Id == Convert.ToInt32(e.Value));
                StateService.ChosenContacts.Add(user);
                OnUpdate?.Invoke();
            }
        }

        public void DeselectContact(int id)
        {
            User user = StateService.ChosenContacts.Find(x => x.Id == id);
            if (user != null)
            {
                StateService.ChosenContacts.Remove(user);
                OnUpdate?.Invoke();
            }
        }
    }
}
