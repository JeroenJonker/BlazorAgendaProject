using BlazorAgenda.Services.Interfaces;
using BlazorAgenda.Shared;
using Microsoft.AspNetCore.Blazor;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAgenda.Services
{
    public class ContactsService : IContactsService
    {
        private readonly HttpClient http;
        public event Action OnChange;
        public List<Contact> CurrentCollection { get; set; }

        public ContactsService(HttpClient client)
        {
            http = client;
        }

        public async Task<List<Contact>> GetContacts()
        {
            CurrentCollection = await http.GetJsonAsync<List<Contact>>("api/Persons/GetContacts");
            return CurrentCollection;
        }

        public void CurrentObjectToNull()
        {
            CurrentCollection = null;
        }

        public void NotifyStateChanged()
        {
            Console.WriteLine("Changed");
            OnChange?.Invoke();
        }

        public Task ExecuteAsync()
        {
            throw new NotImplementedException();
        }
    }
}
