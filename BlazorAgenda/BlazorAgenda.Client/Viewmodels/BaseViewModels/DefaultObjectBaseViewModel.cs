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
        protected IDefaultService Service { get; set; }
        [Parameter]
        protected RenderFragment ChildContent { get; set; }
        [Parameter]
        protected bool IsVisible { get; set; }

        public virtual async Task PostAsync()
        {
            await Service.PostAsync();
        }

        public void Collapse()
        {
            IsVisible = !IsVisible;
        }
    }
}
