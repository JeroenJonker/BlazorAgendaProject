using Microsoft.AspNetCore.Blazor.Components;

namespace BlazorAgenda.Client.Viewmodels
{
    public class LoadingViewModel : BlazorComponent
    {
        public string Title = "Loading";
        [Parameter] protected bool IsVisibile { get; set; }
    }
}
