using BlazorAgenda.Shared;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAgenda.Services
{
    public interface IDefaultObjectService<T> where T : BaseObject
    {
        Task ExecuteAsync();
        void NotifyStateChanged();
        T CurrentObject { get; set; }
    }
}
