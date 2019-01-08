using BlazorAgenda.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorAgenda.Services.Interfaces
{
    public interface IUserService : IDefaultObjectService<User>
    {
        Task<User> CheckUser(User user);
        Task<List<User>> GetContacts();
    }
}
