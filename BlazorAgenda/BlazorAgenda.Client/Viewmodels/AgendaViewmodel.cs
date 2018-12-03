using BlazorAgenda.Client.Views;
using BlazorAgenda.Services;
using Microsoft.AspNetCore.Blazor.Components;
using BlazorAgenda.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorAgenda.Services.Interfaces;
using Microsoft.AspNetCore.Blazor;
using BlazorAgenda.Shared.Models;

namespace BlazorAgenda.Client.Viewmodels
{
    public class AgendaViewmodel : BlazorComponent
    {
        [Inject]
        protected IEventService Service {get;set;}
        [Inject]
        protected IUserService UserService { get; set; }
        public bool IsFocus = true;
        public bool isButtonClicked = false;
        public bool isLoaded = false;
        public RenderFragment LoadComponent { get; set; }

        public void AddNewEvent()
        {
            LoadComponent = builder =>
            {
                builder.OpenComponent(0, typeof(CalendarEventView));
                builder.CloseComponent();
            };
            Service.CurrentEvent = new Event
            {
                Start = DateTime.Now,
                Emailadress = UserService.CurrentUser.Emailadress,
                //EmailadressNavigation = UserService.CurrentUser,
                End = DateTime.Now,
                Summary = "Afspraak"
            };
        }

        protected override void OnInit()
        {
            Service.OnChange += StateHasChanged;
        }

        public void ClickButton()
        {
            isButtonClicked = true;
            StateHasChanged();
        }

        public void ChildLoadedEvent(bool _isLoaded)
        {
            isLoaded = _isLoaded;
            StateHasChanged();
        }
    }
}
