using BlazorAgenda.Services.Interfaces;
using BlazorAgenda.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorAgenda.Services
{
    public class StateService : IStateService
    {
        public User LoginUser { get; set; }

        public StateService(User user)
        {
            LoginUser = user;
        }
    }
}
