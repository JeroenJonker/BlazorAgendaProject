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

        [Parameter]
        protected Action<DateTime> NewEvent { get; set; }

        public string HighlightDropTargetStyle { get; set; }

        public void OnItemDragStart(UIDragEventArgs e, Event calendarEvent)
        {
            DragDropHelper.Item = calendarEvent;
        }

        public void OnContainerDragEnter(UIDragEventArgs e)
        {
            HighlightDropTargetStyle = "background-color: #0069d9 !important;";
        }

        public void OnContainerDragLeave(UIDragEventArgs e)
        {
            HighlightDropTargetStyle = string.Empty;
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

        public void OnContainerClick()
        {
            if(DragDropHelper.Items.FindAll(x => x.Start == Start).Count == 0)
            {
                NewEvent?.Invoke(Start);
            }
        }
    }
}
