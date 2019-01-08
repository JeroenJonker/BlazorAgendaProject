using Microsoft.AspNetCore.Blazor.Components;
using BlazorAgenda.Client.Viewmodels.BaseViewModels;
using System;
using BlazorAgenda.Services.Interfaces;
using BlazorAgenda.Shared.Models;

namespace BlazorAgenda.Client.Viewmodels
{
    public class EventViewmodel : ObjectBase<Event, IEventService>
    {
        [Inject] [Parameter] protected override Event CurrentObject { get; set; }
        [Inject] public override IEventService CurrentService { get; set; }
        [Inject] protected IStateService StateService { get; set; }
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

        protected override void OnInit()
        {
            Start = Start == default ?  SetDateTime(CurrentObject.Start) : Start;
            End = End == default ? SetDateTime(CurrentObject.End) : End;
            CurrentObject.Userid = CurrentObject.Userid == default ? StateService.LoginUser.Id : CurrentObject.Userid;
        }

        protected DateTime SetDateTime(DateTime time)
        {
            return time == default ? DateTime.Now : time;
        }
    }
}
