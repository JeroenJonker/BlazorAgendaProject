using Microsoft.AspNetCore.Blazor.Components;
using Microsoft.AspNetCore.Blazor;
using System.Threading.Tasks;
using BlazorAgenda.Shared;
using BlazorAgenda.Services;
using BlazorAgenda.Client.Viewmodels.BaseViewModels;
using System;
using BlazorAgenda.Services.Interfaces;
using BlazorAgenda.Shared.Models;

namespace BlazorAgenda.Client.Viewmodels
{
    public class EventViewmodel : ObjectBase
    {
        [Inject] protected IEventService EventService { get; set; }
        [Parameter] protected Event SetEvent { get; set; }
        public DateTime Start
        {
            get
            {
                return EventService.CurrentEvent.Start;
            }
            set
            {
                if (EventService.CurrentEvent.Start.Date != value.Date)
                {
                    EventService.CurrentEvent.Start = new DateTime(value.Year, value.Month, value.Day, 
                        EventService.CurrentEvent.Start.Hour, EventService.CurrentEvent.Start.Minute, EventService.CurrentEvent.Start.Second);
                }
                else
                {
                    EventService.CurrentEvent.Start = value;
                }
            }
        }

        public DateTime End
        {
            get
            {
                return EventService.CurrentEvent.End;
            }
            set
            {
                if (EventService.CurrentEvent.End.Date != value.Date)
                {
                    EventService.CurrentEvent.End = new DateTime(value.Year, value.Month, value.Day, 
                        EventService.CurrentEvent.End.Hour, EventService.CurrentEvent.End.Minute, EventService.CurrentEvent.End.Second);
                }
                else
                {
                    EventService.CurrentEvent.End = value;
                }
            }
        }

        protected override void OnInit()
        {
            if (SetEvent != null)
            {
                EventService.CurrentEvent = SetEvent;
            }
            else
            {
                Start = SetDateTime(EventService.CurrentEvent.Start);
                End = SetDateTime(EventService.CurrentEvent.End);
            }

            //Service.CurrentEvent.Emailadress = Service.CurrentEvent.Emailadress == default ? StateService.LoginUser.Emailadress : Service.CurrentEvent.Emailadress;
        }

        protected DateTime SetDateTime(DateTime time)
        {
            return time == default ? DateTime.Now : time;
        }
    }
}
