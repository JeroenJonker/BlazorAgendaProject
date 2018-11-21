using BlazorAgenda.Client.Views;
using BlazorAgenda.Services;
using Microsoft.AspNetCore.Blazor.Components;
using BlazorAgenda.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorAgenda.Client.Viewmodels
{
    public class AgendaViewmodel : BlazorComponent
    {
        [Inject]
        protected CalendarEventService Service {get;set;}
        public bool IsFocus = true;
        public bool isButtonClicked = false;
        public bool isLoaded = false;

        public void AddNewEvent()
        {
            Service.CurrentObject = new CalendarEvent
            {
                Start = DateTime.Now,
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
        }

        public void ChildLoadedEvent(bool _isLoaded)
        {
            isLoaded = _isLoaded;
            StateHasChanged();
        }
    }
}
