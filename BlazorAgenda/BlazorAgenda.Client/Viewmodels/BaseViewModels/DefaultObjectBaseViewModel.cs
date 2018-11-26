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
        [Parameter]
        protected RenderFragment ChildContent { get; set; }
        [Inject] protected ServiceInterface Service { get; set; }

        public void Close()
        {
            ((IDefaultObjectService<BaseObject>)Service).CurrentObjectToNull();
            ((IDefaultObjectService<BaseObject>)Service).NotifyStateChanged();
        }

        public virtual async Task Save()
        {
            await ((IDefaultObjectService<BaseObject>)Service).ExecuteAsync();
            Close();
        }
    }
}
