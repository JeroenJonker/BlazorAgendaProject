using BlazorAgenda.Services.Interfaces;
using BlazorAgenda.Shared;
using BlazorAgenda.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAgenda.Services
{
    public class StateService : IStateService
    {
        public User LoginUser { get; set; }

        public List<User> ChosenContacts { get; set; }

        public Type ObjectFocus { get; set; }

        public StateService(User user)
        {
            LoginUser = user;
        }
    }
}
