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
    public class EventViewmodel : ObjectBase<Event, IEventService>
    {
        [Inject] [Parameter] protected override Event CurrentObject { get; set; }
        [Inject] public override IEventService CurrentService { get; set; }
        [Inject] protected IStateService StateService { get; set; }

        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public DateTime StartDate
        {
            get
            {
                return Start;
            }
            set
            {
                Start = new DateTime(value.Year, value.Month, value.Day, Start.Hour,
                    Start.Minute, Start.Second);
            }
        }

        public DateTime StartTime
        {
            get
            {
                return Start;
            }
            set
            {
                Start = new DateTime(Start.Year, Start.Month, Start.Day,
                        value.Hour, value.Minute, value.Second);
            }
        }
        public DateTime EndDate
        {
            get
            {
                return CurrentObject.End;
            }
            set
            {
                End = new DateTime(value.Year, value.Month, value.Day, End.Hour,
                    End.Minute, End.Second);
            }
        }
        public DateTime EndTime
        {
            get
            {
                return End;
            }
            set
            {
                End = new DateTime(End.Year, End.Month, End.Day,
                        value.Hour, value.Minute, value.Second);
            }
        }

        protected override void OnInit()
        {
            Start = (Start == default) ? SetDateTime(CurrentObject.Start) : Start;
            End = (End == default) ? SetDateTime(CurrentObject.End) : End;
            CurrentObject.Userid = CurrentObject.Userid == default ? StateService.LoginUser.Id : CurrentObject.Userid;
        }

        protected DateTime SetDateTime(DateTime time)
        {
            return time == default ? DateTime.Now : time;
        }

        public async void SaveEvent()
        {
            CurrentObject.Start = Start;
            CurrentObject.End = End;
            await Save();
        }
    }
}
