﻿using BlazorAgenda.Shared;
using BlazorAgenda.Services;
using Microsoft.AspNetCore.Blazor;
using Microsoft.AspNetCore.Blazor.Components;
using Microsoft.AspNetCore.Blazor.RenderTree;
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
        protected double Top { get; set; }

        [Parameter]
        protected double Left { get; set; }

        [Parameter]
        protected double Height { get; set; }

        [Parameter]
        protected double Width { get; set; }

        public void PrintSummary()
        {
            Console.WriteLine(Event.Summary);
        }
    }
}
