using Microsoft.AspNetCore.Blazor.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorAgenda.Client.Viewmodels
{
    public class LoadingViewModel : BlazorComponent
    {
        public string Title = "Loading";
        [Parameter] protected bool IsVisibile { get; set; }
    }
}
