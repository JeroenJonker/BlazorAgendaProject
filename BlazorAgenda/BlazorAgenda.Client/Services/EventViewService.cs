﻿using BlazorAgenda.Services;
using BlazorAgenda.Services.Interfaces;
using BlazorAgenda.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public EventViewService(Event currentEvent, IEventService eventService, IStateService stateService)
        {
            CurrentObject = currentEvent;
            CurrentService = eventService;
            StateService = StateService;
            Start = Start == default ? SetDateTime(CurrentObject.Start) : Start;
            End = End == default ? SetDateTime(CurrentObject.End) : End;
            //CurrentObject.Userid = CurrentObject.Userid == default ? StateService.LoginUser.Id : CurrentObject.Userid;
        }

        protected DateTime SetDateTime(DateTime time)
        {
            return time == default ? DateTime.Now : time;
        }
    }
}
