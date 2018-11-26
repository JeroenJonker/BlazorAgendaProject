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

namespace BlazorAgenda.Client.Viewmodels
{
    public class AgendaViewmodel : BlazorComponent
    {
        [Inject]
        protected ICalendarEventService Service {get;set;}
        public bool IsFocus = true;
        public bool isButtonClicked = false;
        public bool isLoaded = false;
        public RenderFragment LoadComponent { get; set; }
        //public string myMarkup = "<CalendarEventView/>"; //string.Format("<{0}/>", CalendarEventView);

        public void AddNewEvent()
        {
            LoadComponent = builder =>
            {
                builder.OpenComponent(0, typeof(CalendarEventView));
                //builder.AddAttribute
                builder.CloseComponent();
            };
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
