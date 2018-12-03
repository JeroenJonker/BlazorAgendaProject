using Microsoft.AspNetCore.Blazor.Components;
using Microsoft.AspNetCore.Blazor;
using System.Threading.Tasks;
using BlazorAgenda.Shared;
using BlazorAgenda.Services;
using BlazorAgenda.Client.Viewmodels.BaseViewModels;
using System;
using BlazorAgenda.Services.Interfaces;

namespace BlazorAgenda.Client.Viewmodels
{
    public class CalendarEventViewmodel : BlazorComponent
    {
        [Inject]
        protected IEventService Service { get; set; }
        public DateTime Start
        {
            get
            {
                return Service.CurrentEvent.Start;
            }
            set
            {
                if (Service.CurrentEvent.Start.Date != value.Date)
                {
                    Service.CurrentEvent.Start = new DateTime(value.Year, value.Month, value.Day, 
                        Service.CurrentEvent.Start.Hour, Service.CurrentEvent.Start.Minute, Service.CurrentEvent.Start.Second);
                }
                else
                {
                    Service.CurrentEvent.Start = value;
                }
            }
        }

        public DateTime End
        {
            get
            {
                return Service.CurrentEvent.End;
            }
            set
            {
                if (Service.CurrentEvent.End.Date != value.Date)
                {
                    Service.CurrentEvent.End = new DateTime(value.Year, value.Month, value.Day, 
                        Service.CurrentEvent.End.Hour, Service.CurrentEvent.End.Minute, Service.CurrentEvent.End.Second);
                }
                else
                {
                    Service.CurrentEvent.End = value;
                }
            }
        }

        protected override void OnInit()
        {
            Service.OnChange += StateHasChanged;
        }
    }
}
