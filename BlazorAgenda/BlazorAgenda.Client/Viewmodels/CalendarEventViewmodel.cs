using BlazorAgenda.Shared;
using BlazorAgenda.Services;
using Microsoft.AspNetCore.Blazor;
using Microsoft.AspNetCore.Blazor.Components;
using Microsoft.AspNetCore.Blazor.RenderTree;
using Microsoft.JSInterop;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorAgenda.Shared.Models;
using BlazorAgenda.Services.Interfaces;
using BlazorAgenda.Client.Services;
using BlazorAgenda.Shared.Interfaces;

namespace BlazorAgenda.Client.Viewmodels
{
    public class CalendarEventViewModel : BlazorComponent
    {
        [Inject]
        protected EventViewService UserView { get; set; }

        [Inject]
        protected IStateService StateService { get; set; }

        [Parameter]
        protected ICalendarEvent CalendarEvent { get; set; }

        [Parameter]
        protected int Rowspan { get; set; }

        [Parameter]
        protected int NumEvents { get; set; }

        [Parameter]
        protected Action<UIDragEventArgs, CalendarEvent> DragStart { get; set; }

        public bool ShowModalEvent { get; set; } = false;

        public void ChangeShowModalEvent()
        {
            if (CalendarEvent.Event.Userid == StateService.LoginUser.Id)
            {
                UserView.CurrentObject = CalendarEvent.Event as Event;
                UserView.ChangeVisibility();
            }
        }
    }
}
