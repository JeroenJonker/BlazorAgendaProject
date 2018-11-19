using BlazorAgenda.Services;
using BlazorAgenda.Shared;
using Microsoft.AspNetCore.Blazor.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorAgenda.Client.Viewmodels.BaseViewModels
{
    public abstract class DefaultObjectBaseViewModel<T> : BlazorComponent where T : IDefaultService
    {
        [Inject]
        protected T CurrentObject { get; set; }

        public virtual async Task PostAsync()
        {
            await CurrentObject.PostAsync();
        }
    }
}
