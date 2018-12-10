using BlazorAgenda.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAgenda.Services.Interfaces
{
    public interface IEventService : IDefaultObjectService
    {
        Event CurrentEvent { get; set; }
        Task<List<Event>> GetEvents(User user);
    }
}
