using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace BlazorAgenda.Services.Interfaces
{
    public abstract class DefaultObjectService
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
    }
}
