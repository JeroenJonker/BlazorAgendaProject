using BlazorAgenda.Services.Interfaces;
using BlazorAgenda.Shared.Models;
using Microsoft.AspNetCore.Blazor;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAgenda.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient http;

        public User CurrentUser { get; set; }

        public UserService(HttpClient http)
        {
            this.http = http;
        }

        public async Task<bool> AddUser()
        {
            return await http.PostJsonAsync<bool>("api/User/AddUser", CurrentUser);
        }

        public async Task<bool> CheckUser()
        {
            if (await http.PostJsonAsync<bool>("api/User/IsValidUser", CurrentUser))
            {
                CurrentUser.Event = await http.GetJsonAsync<List<Event>>("api/Event/GetUserEvents/" + CurrentUser.Emailadress);
                return true;
            }
            return false;
        }

        public byte[] ConvertStringToHash(string text)
        {
            if (string.IsNullOrEmpty(text))
                return default(byte[]);

            using (var sha = new SHA256Managed())
            {
                byte[] textData = Encoding.UTF8.GetBytes(text);
                return sha.ComputeHash(textData);
            }
        }
    }
}
