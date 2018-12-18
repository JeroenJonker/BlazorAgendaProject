using BlazorAgenda.Services;
using BlazorAgenda.Services.Interfaces;
using BlazorAgenda.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorAgenda.Client.Services
{
    public class UserViewService : BaseObjectViewService<User, IUserService>
    {
        public override User CurrentObject { get; set; }
        public override User DefaultBaseObject { get; set; }
        public override IUserService CurrentService { get; set; }

        public UserViewService(User user, IUserService userService)
        {
            DefaultBaseObject = CurrentObject = user;
            CurrentService = userService;
        }
    }
}
