using BlazorAgenda.Client.Services;
using BlazorAgenda.Client.Views;
using BlazorAgenda.Shared;
using BlazorAgenda.Shared.Models;
using Microsoft.AspNetCore.Blazor;
using Microsoft.AspNetCore.Blazor.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorAgenda.Client.Viewmodels
{
    public class DragDropViewModel : BlazorComponent
    {
        [Parameter]
        protected DateTime Start { get; set; }

        [Parameter]
        protected Action<Event> MoveEvent { get; set; }

        public string HighlightDropTargetStyle { get; set; }

        public void OnItemDragStart(UIDragEventArgs e, Event calendarEvent)
        {
            DragDropHelper.Item = calendarEvent;
        }

        public void OnContainerDragEnter(UIDragEventArgs e)
        {
            HighlightDropTargetStyle = "border-color: black; background-color: " + DragDropHelper.Item.Color + " !important;";
        }

        public void OnContainerDragLeave(UIDragEventArgs e)
        {
            HighlightDropTargetStyle = "";
        }

        public void OnContainerDrop(UIDragEventArgs e, DateTime _start)
        {
            HighlightDropTargetStyle = "";
            Event item = DragDropHelper.Item;
            TimeSpan duration = item.End - item.Start;
            item.Start = _start;
            item.End = _start.Add(duration);
            MoveEvent?.Invoke(item);
        }
    }
}
