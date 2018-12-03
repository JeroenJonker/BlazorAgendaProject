using BlazorAgenda.Client.Services;
using BlazorAgenda.Client.Views;
using BlazorAgenda.Shared;
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
        protected Action OnChange { get; set; }

        [Parameter]
        protected DateTime Start { get; set; }

        public string HighlightDropTargetStyle { get; set; }

        public void OnItemDragStart(UIDragEventArgs e, CalendarEvent calendarEvent)
        {
            DragDropHelper.Item = calendarEvent;
        }

        public void OnContainerDragEnter(UIDragEventArgs e)
        {
            HighlightDropTargetStyle = "background-color: salmon !important";
        }

        public void OnContainerDragLeave(UIDragEventArgs e)
        {
            HighlightDropTargetStyle = "";
        }

        public void OnContainerDrop(UIDragEventArgs e, DateTime _start)
        {
            HighlightDropTargetStyle = "";
            DragDropHelper.Item.Start = _start;
            OnChange();
        }
    }
}
