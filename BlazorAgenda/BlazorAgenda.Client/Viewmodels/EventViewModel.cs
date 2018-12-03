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

namespace BlazorAgenda.Client.Viewmodels
{
    public class EventViewModel : BlazorComponent
    {
        [Parameter]
        protected Color EventColor { get; set; }

        [Parameter]
        protected CalendarEvent Event { get; set; }

        [Parameter]
        protected int Row { get; set; }

        [Parameter]
        protected int Column { get; set; }

        [Parameter]
        protected int Rowspan { get; set; }

        [Parameter]
        protected int NumEvents { get; set; }

        public double Top { get; set; }
        public double Left { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }

        protected override async Task OnInitAsync()
        {
            Top = await JSRuntime.Current.InvokeAsync<double>("interopFunctions.getOffsetTop", Row) + 1;
            Left = await JSRuntime.Current.InvokeAsync<double>("interopFunctions.getOffsetLeft", Row, Column) + 1;
            Width = await JSRuntime.Current.InvokeAsync<double>("interopFunctions.getColumnWidth", Row, Column) - 1;
            if(NumEvents > 0)
                Width = Width / NumEvents;
            Height = (Rowspan * 16.8d) - 1;
        }

        public void PrintSummary()
        {
            Console.WriteLine(Event.Summary);
        }

        public void OnDragStart()
        {

        }
    }
}
