using BlazorAgenda.Shared;
using BlazorAgenda.Shared.Models;
using Microsoft.AspNetCore.Blazor;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAgenda.Services.Interfaces
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
                await http.PutJsonAsync("api/" + GetObjectName(CurrentObject) + "/Edit", CurrentObject);
            }
            else
            {
                await http.PostJsonAsync("api/"+ GetObjectName(CurrentObject) + "/Add", CurrentObject);
            }
        }

        public string GetObjectName(T CurrentObject)
        {
            return typeof(T).Name.ToString();
        }

        public async Task Delete(T CurrentObject)
        {
            await http.SendJsonAsync(HttpMethod.Delete, "api/" + GetObjectName(CurrentObject) + "/Delete", CurrentObject);
        }
    }
}
