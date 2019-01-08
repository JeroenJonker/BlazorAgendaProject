using BlazorAgenda.Shared;
using BlazorAgenda.Shared.Enums;
using BlazorAgenda.Shared.Interfaces.BaseObjects;
using BlazorAgenda.Shared.Models;
using BlazorAgenda.Shared.Properties;
using Microsoft.AspNetCore.Blazor;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace BlazorAgenda.Services
{
    public abstract class DefaultObjectService<T> where T : IBaseObject
    {
        protected readonly HttpClient http;

        public event Action OnChange;

        public DefaultObjectService(HttpClient client)
        {
            http = client;
        }

        public void NotifyStateChanged()
        {
            OnChange?.Invoke();
        }

        public virtual ObjectState GetObjectState(T CurrentObject)
        {
            return CurrentObject.Id != default(int) ? ObjectState.Edit : ObjectState.Add;
        }

        public virtual async Task ExecuteAsync(T CurrentObject)
        {
            if (GetObjectState(CurrentObject) == ObjectState.Edit)
            {
                await http.PutJsonAsync(Resources.ControllerApi + GetObjectName(CurrentObject) + Resources.ObjectApi_Edit, CurrentObject);
            }
            else
            {
                await http.PostJsonAsync(Resources.ControllerApi + GetObjectName(CurrentObject) + Resources.ObjectApi_Add, CurrentObject);
            }
        }

        public string GetObjectName(T CurrentObject)
        {
            return typeof(T).Name.ToString();
        }

        public async Task Delete(T CurrentObject)
        {
            await http.SendJsonAsync(HttpMethod.Delete, Resources.ControllerApi + GetObjectName(CurrentObject) + Resources.ObjectApi_Delete, CurrentObject);
        }
    }
}
