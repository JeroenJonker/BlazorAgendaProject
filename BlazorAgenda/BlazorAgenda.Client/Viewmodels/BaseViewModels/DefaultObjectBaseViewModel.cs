using BlazorAgenda.Services;
using BlazorAgenda.Shared;
using Microsoft.AspNetCore.Blazor;
using Microsoft.AspNetCore.Blazor.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorAgenda.Client.Viewmodels.BaseViewModels
{
    public class DefaultObjectBaseViewModel: BlazorComponent
    {
        [Parameter]
        protected BaseObject CurrentObject { get; set; }
        [Parameter]
        protected RenderFragment ChildContent { get; set; }
        [Parameter]
        internal bool IsVisible { get; set; }
        [Parameter] protected Action<BaseObject> Post { get; set; }

        public void Close()
        {
            CurrentObject = null;
            Post(CurrentObject as CalendarEvent);
        }

        public virtual async Task Save()
        {
            //await Service.PostAsync();
            Close();
        }
    }
}
