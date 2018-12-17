using BlazorAgenda.Shared.Models;
using Microsoft.AspNetCore.Blazor.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorAgenda.Client.Viewmodels.BaseViewModels
{
    public class ObjectBaseFooterViewModel : BlazorComponent
    {
        [Parameter] protected Action OnSave { get; set; }
        [Parameter] protected Action OnDelete { get; set; }
        [Parameter] protected ObjectState ObjectState { get; set; }
    }
}
