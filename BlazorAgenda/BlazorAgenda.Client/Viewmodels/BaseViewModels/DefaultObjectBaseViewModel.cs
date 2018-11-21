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
    public class DefaultObjectBaseViewModel<BaseObjectType> : BlazorComponent
    {
        [Parameter]
        protected RenderFragment ChildContent { get; set; }
        [Parameter] protected IDefaultObjectService<BaseObjectType> Service { get; set; }

        public void Close()
        {
            Service.CurrentObject = default;
            Service.NotifyStateChanged();
        }

        public virtual async Task Save()
        {
            await Service.PostAsync();
            Close();
        }
    }
}
