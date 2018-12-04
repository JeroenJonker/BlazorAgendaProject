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

namespace BlazorAgenda.Client.Viewmodels
{
    public class EventViewModel : BlazorComponent
    {
        [Parameter]
        protected Event Event { get; set; }

        [Parameter]
        protected int Rowspan { get; set; }

        [Parameter]
        protected int NumEvents { get; set; }

        [Parameter]
        protected Action<UIDragEventArgs, Event> DragStart { get; set; }

        public void PrintSummary()
        {
            Console.WriteLine(Event.Summary);
        }
    }
}
