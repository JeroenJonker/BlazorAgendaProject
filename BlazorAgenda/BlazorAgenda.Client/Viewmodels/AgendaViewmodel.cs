using BlazorAgenda.Client.Views;
using Microsoft.AspNetCore.Blazor.Components;
using System;
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
        [Inject]
        protected IStateService StateService { get; set; }
        public bool IsFocus = true;
        public bool isButtonClicked = false;
        public bool isLoaded = false;
        public RenderFragment LoadComponent { get; set; }

        public void AddNewEvent()
        {
            LoadComponent = builder =>
            {
                builder.OpenComponent(0, typeof(EventView));
                builder.CloseComponent();
            };
            Service.CurrentEvent.Emailadress = StateService.LoginUser.Emailadress;
        }

        public void EditUser()
        {
            LoadComponent = builder =>
            {
                builder.OpenComponent(0, typeof(UserView));
                builder.AddAttribute(1, "SetUser", StateService.LoginUser);
                builder.CloseComponent();
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
