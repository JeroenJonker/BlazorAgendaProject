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
    public class ModalTemplateViewModel : BlazorComponent
    {
        [Parameter] protected RenderFragment ModalContent { get; set; }
        [Parameter] Action OnClose { get; set; }
        [Parameter] protected bool IsVisible { get; set; }
        [Parameter] protected string Title { get; set; }
        [Parameter] protected RenderFragment FooterContent { get; set; }

        public void Close()
        {
            OnClose?.Invoke();
            IsVisible = false;
        }
    }
}
