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
        //[Parameter]
        //protected CalendarEvent CalendarEvent { get; set; }

        [Inject]
        protected ICalendarEventService Service { get; set; }

        //[Parameter] protected Action<BaseObject> Post { get; set; }

        protected override void OnInit()
        {
            Service.OnChange += StateHasChanged;
        }

    }
}
