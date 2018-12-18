using BlazorAgenda.Services;
using BlazorAgenda.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorAgenda.Client.Services
{
    public abstract class BaseObjectViewService<BaseObject, BaseService> where BaseObject : IBaseObject where BaseService : IDefaultObjectService<BaseObject>
    {
        public abstract BaseObject CurrentObject { get; set; }
        public abstract BaseService CurrentService { get; set; }
        public abstract BaseObject DefaultBaseObject { get; set; }
        public Action OnClose { get; set; }
        public bool IsVisible { get; set; }

        public event Action OnChange;

        public void SetCurrentObjectToDefault()
        {
            CurrentObject = DefaultBaseObject;
        }

        public string Title
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
            SetCurrentObjectToDefault();
            ChangeVisibility();
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

        public void ChangeVisibility()
        {
            IsVisible = !IsVisible;
            NotifyStateChanged();
        }

        public void NotifyStateChanged() => OnChange?.Invoke();
    }
}
