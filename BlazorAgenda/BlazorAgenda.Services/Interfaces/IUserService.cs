using BlazorAgenda.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAgenda.Services.Interfaces
{
    public interface IUserService : IDefaultObjectService
    {
        User CurrentUser { get; set; }
        Task<User> CheckUser(User user);
        Task<List<User>> GetContacts();
        string ConvertStringToHash(string text);
    }
}
