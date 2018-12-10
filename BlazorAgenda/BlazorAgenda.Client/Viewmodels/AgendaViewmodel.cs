﻿using BlazorAgenda.Client.Views;
using Microsoft.AspNetCore.Blazor.Components;
using System;
using BlazorAgenda.Services.Interfaces;
using Microsoft.AspNetCore.Blazor;
using BlazorAgenda.Shared.Models;

namespace BlazorAgenda.Client.Viewmodels
{
    public class AgendaViewmodel : BlazorComponent
    {
        [Inject]
        protected IStateService StateService { get; set; }
        public bool IsFocus = true;
        public bool IsLoginCompleted = false;
        public bool isLoaded = false;

        public PopupView _popup;

        public void AddEvent()
        {
            StateService.ObjectFocus = typeof(Event);
        }

        public void EditUser()
        {
            StateService.ObjectFocus = typeof(User);
        }

        public void OnCloseDialog()
        {
            StateService.ObjectFocus = null;
            StateHasChanged();
        }

        public void OnLoginCompleted(User user)
        {
            StateService.LoginUser = user;
            IsLoginCompleted = true;
            StateHasChanged();
        }

        public void ChildLoadedEvent(bool _isLoaded)
        {
            isLoaded = _isLoaded;
            StateHasChanged();
        }

        public void OpenPopup()
        {
            _popup.Open();
            StateHasChanged();
        }
    }
}
