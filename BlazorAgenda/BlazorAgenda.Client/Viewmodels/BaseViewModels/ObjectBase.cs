using Microsoft.AspNetCore.Blazor.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorAgenda.Client.Viewmodels.BaseViewModels
{
    public class ObjectBase : BlazorComponent
    {
        [Parameter] protected Action OnClose { get; set; }
        [Parameter] protected bool IsVisible { get; set; }
    }
}
