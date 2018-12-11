using BlazorAgenda.Services;
using BlazorAgenda.Shared;
using BlazorAgenda.Shared.Models;
using Microsoft.AspNetCore.Blazor;
using Microsoft.AspNetCore.Blazor.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorAgenda.Client.Viewmodels.BaseViewModels
{
    public class DefaultObjectBaseViewModel<ServiceInterface> : BlazorComponent
    {
        [Parameter] protected RenderFragment ChildContent { get; set; }
        [Inject] protected ServiceInterface Service { get; set; }
        [Parameter] protected Action BeforeSave { get; set; }
        [Parameter] protected bool IsVisible { get; set; } = false;
        [Parameter] protected Action GetClosedValue { get; set; }
        
        public string GetTitle()
        {
            return ((IDefaultObjectService)Service).GetObjectState().ToString() + " " + ((IDefaultObjectService)Service).GetObjectName();
        }

        public void Close()
        {
            //((IDefaultObjectService)Service).CurrentObjectToNull();
            IsVisible = false;
            GetClosedValue?.Invoke();
            ((IDefaultObjectService)Service).NotifyStateChanged();
        }

        public virtual async Task Save()
        {
            BeforeSave?.Invoke();
            await ((IDefaultObjectService)Service).ExecuteAsync();
            Close();
        }
    }
}
