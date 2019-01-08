using BlazorAgenda.Services.Interfaces;
using BlazorAgenda.Shared.Interfaces;
using BlazorAgenda.Shared.Models;
using System;

namespace BlazorAgenda.Client.Services
{
    public class EventViewService : BaseObjectViewService<Event, IEventService>
    {
        public override IEventService CurrentService { get ; set; }
        public override Event CurrentObject { get; set; }
        public IStateService StateService { get; set; }
        public DateTime Start
        {
            get
            {
                return CurrentObject.Start;
            }
            set
            {
                if (CurrentObject.Start.Date != value.Date)
                {
                    CurrentObject.Start = new DateTime(value.Year, value.Month, value.Day,
                        CurrentObject.Start.Hour, CurrentObject.Start.Minute, CurrentObject.Start.Second);
                }
                else
                {
                    CurrentObject.Start = value;
                }
            }
        }

        public DateTime End
        {
            get
            {
                return CurrentObject.End;
            }
            set
            {
                if (CurrentObject.End.Date != value.Date)
                {
                    CurrentObject.End = new DateTime(value.Year, value.Month, value.Day,
                        CurrentObject.End.Hour, CurrentObject.End.Minute, CurrentObject.End.Second);
                }
                else
                {
                    CurrentObject.End = value;
                }
            }
        }

        public override Event DefaultBaseObject { get; set; }

        public EventViewService(IEvent currentEvent, IEventService eventService, IStateService stateService)
        {
            DefaultBaseObject = CurrentObject = currentEvent as Event;
            CurrentService = eventService;
            StateService = StateService;
            Start = Start == default ? SetDateTime(CurrentObject.Start) : Start;
            End = End == default ? SetDateTime(CurrentObject.End) : End;
        }

        protected DateTime SetDateTime(DateTime time)
        {
            return time == default ? DateTime.Now : time;
        }
    }
}
