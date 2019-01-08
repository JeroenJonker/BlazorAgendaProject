using BlazorAgenda.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorAgenda.Services.Interfaces
{
    public interface IEventService : IDefaultObjectService<Event>
    {
        Task<List<Event>> GetEvents(User user);
    }
}
