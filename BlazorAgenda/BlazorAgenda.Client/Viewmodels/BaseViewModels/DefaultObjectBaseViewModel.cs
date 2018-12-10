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
    public class DefaultObjectBaseViewModel<ServiceInterface> : BlazorComponent
    {
        [Parameter] protected RenderFragment ChildContent { get; set; }
        [Inject] protected ServiceInterface Service { get; set; }
        [Parameter] protected Action BeforeSave { get; set; }
        [Parameter] protected bool IsVisible { get; set; } = false;
        [Parameter] protected Action GetClosedValue { get; set; }

        public void Close()
        {
            Console.WriteLine("CLose1");
            //((IDefaultObjectService)Service).CurrentObjectToNull();
            IsVisible = false;
            Console.WriteLine("CLose2");
            GetClosedValue?.Invoke();
            Console.WriteLine("CLose3");
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
