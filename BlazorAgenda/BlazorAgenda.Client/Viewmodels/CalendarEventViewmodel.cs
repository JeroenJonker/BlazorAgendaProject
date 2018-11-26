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
        protected ICalendarEventService Service { get; set; }
        public DateTime Start
        {
            get
            {
                return Service.CurrentObject.Start;
            }
            set
            {
                if (Service.CurrentObject.Start.Date != value.Date)
                {
                    Service.CurrentObject.Start = new DateTime(value.Year, value.Month, value.Day, 
                        Service.CurrentObject.Start.Hour, Service.CurrentObject.Start.Minute, Service.CurrentObject.Start.Second);
                }
                else
                {
                    Service.CurrentObject.Start = value;
                }
            }
        }

        public DateTime End
        {
            get
            {
                return Service.CurrentObject.End;
            }
            set
            {
                if (Service.CurrentObject.End.Date != value.Date)
                {
                    Service.CurrentObject.End = new DateTime(value.Year, value.Month, value.Day, 
                        Service.CurrentObject.End.Hour, Service.CurrentObject.End.Minute, Service.CurrentObject.End.Second);
                }
                else
                {
                    Service.CurrentObject.End = value;
                }
            }
        }

        protected override void OnInit()
        {
            Service.OnChange += StateHasChanged;
        }

    }
}
