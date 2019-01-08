using BlazorAgenda.Services;
using BlazorAgenda.Shared;
using Microsoft.AspNetCore.Blazor.Components;
using System;

namespace BlazorAgenda.Client.Viewmodels.BaseViewModels
{
    public abstract class ObjectBase<BaseObject, BaseService> : BlazorComponent where BaseObject : IBaseObject where BaseService : IDefaultObjectService<BaseObject>
    {
        [Parameter] protected Action OnClose { get; set; }
        [Parameter] protected bool IsVisible { get; set; }
        protected abstract BaseObject CurrentObject { get; set; }
        public abstract BaseService CurrentService { get; set; }
        protected string Title
        {
            get { return GetTitle(); }
        }

        public virtual string GetTitle()
        {
            return CurrentService.GetObjectState(CurrentObject).ToString() + " " + CurrentService.GetObjectName(CurrentObject);
        }

        public void Close()
        {
            OnClose?.Invoke();
            IsVisible = false;
            StateHasChanged();
            CurrentService.NotifyStateChanged();
        }

        public virtual async void Save()
        {
            await CurrentService.ExecuteAsync(CurrentObject);
            Close();
        }

        public virtual async void Delete()
        {
            await CurrentService.Delete(CurrentObject);
            Close();
        }
    }
}
