using BlazorAgenda.Shared;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAgenda.Services.Interfaces
{
    public interface IContactsService : IDefaultObjectService
    {
        List<Contact> CurrentCollection { get; set; }
        Task<List<Contact>> GetContacts();
    }
}
