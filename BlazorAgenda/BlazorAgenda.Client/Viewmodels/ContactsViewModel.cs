using BlazorAgenda.Services.Interfaces;
using BlazorAgenda.Shared.Models;
using Microsoft.AspNetCore.Blazor.Components;
using System;
using System.Collections.Generic;

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

        [Parameter]
        protected List<User> Contacts { get; set; }

        protected override void OnInit()
        {
            Contacts = new List<User>();
            base.OnInit();
        }

        public void SelectContact(int userId, bool selected)
        {
            User user = StateService.ChosenContacts.Find(x => x.Id == userId);
            if (selected && user == null)
            {
                user = Contacts.Find(x => x.Id == userId);
                StateService.ChosenContacts.Add(user);
            }
            else if (!selected && user != null)
            {
                StateService.ChosenContacts.Remove(user);
            }
            OnUpdate?.Invoke();
        }
    }
}
