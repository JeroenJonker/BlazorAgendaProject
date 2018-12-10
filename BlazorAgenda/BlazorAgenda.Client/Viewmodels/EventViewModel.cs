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
    public class EventViewmodel : BlazorComponent
    {
        [Inject]
        protected IEventService Service { get; set; }
        [Inject]
        protected IStateService StateService {get;set;}
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
            Start = SetDateTime(Service.CurrentEvent.Start);
            End = SetDateTime(Service.CurrentEvent.End);
            Service.CurrentEvent.Emailadress = Service.CurrentEvent.Emailadress == default ? StateService.LoginUser.Emailadress : Service.CurrentEvent.Emailadress;
            Service.OnChange += StateHasChanged;
        }

        protected DateTime SetDateTime(DateTime time)
        {
            return time == default ? DateTime.Now : time;
        }
    }
}
