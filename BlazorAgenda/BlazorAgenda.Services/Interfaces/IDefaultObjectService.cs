using BlazorAgenda.Shared;
using BlazorAgenda.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAgenda.Services
{
    public interface IDefaultObjectService<T> where T : IBaseObject
    {
        Task ExecuteAsync(T CurrentObject);
        event Action OnChange;
        void NotifyStateChanged();
        void CurrentObjectToNull(T CurrentObject);
        ObjectState GetObjectState(T CurrentObject);
        string GetObjectName();
        //T CurrentObject { get; set; }
    }
}
