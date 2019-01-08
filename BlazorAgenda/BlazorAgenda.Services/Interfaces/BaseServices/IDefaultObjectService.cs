using BlazorAgenda.Shared;
using BlazorAgenda.Shared.Interfaces.BaseObjects;
using BlazorAgenda.Shared.Enums;
using BlazorAgenda.Shared.Models;
using System;
using System.Threading.Tasks;

namespace BlazorAgenda.Services
{
    public interface IDefaultObjectService<T> where T : IBaseObject
    {
        Task ExecuteAsync(T CurrentObject);
        Task Delete(T CurrentObject);
        event Action OnChange;
        void NotifyStateChanged();
        ObjectState GetObjectState(T CurrentObject);
        string GetObjectName(T CurrentObject);
    }
}
