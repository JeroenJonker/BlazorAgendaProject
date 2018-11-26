using BlazorAgenda.Shared;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAgenda.Services
{
    public interface IDefaultObjectService
    {
        Task ExecuteAsync();
        event Action OnChange;
        void NotifyStateChanged();
        void CurrentObjectToNull();
        //T CurrentObject { get; set; }
    }
}
