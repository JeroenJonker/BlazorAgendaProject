using BlazorAgenda.Services.Interfaces;
using BlazorAgenda.Shared.Models;
using Microsoft.AspNetCore.Blazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAgenda.Services
{
    public class UserService : DefaultObjectService, IUserService
    {
        public User CurrentUser { get; set; }

        public UserService(HttpClient client, User currentUser) : base(client)
        {
            CurrentUser = currentUser;
        }

        public async Task<bool> CheckUser(User user)
        {
            user.Password = ConvertStringToHash(user.Password);
            return await http.PostJsonAsync<bool>("api/User/IsValidUser", user);
        }

        public async Task<List<User>> GetContacts()
        {
            return await http.GetJsonAsync<List<User>>("api/User/GetAllUsers");
        }

        public string ConvertStringToHash(string text)
        {
            if (string.IsNullOrEmpty(text))
                return default(string);

            using (var sha = new SHA256Managed())
            {
                byte[] textData = Encoding.UTF8.GetBytes(text);
                byte[] hash = sha.ComputeHash(textData);
                return BitConverter.ToString(hash).Replace("-", string.Empty);
            }
        }

        public async Task ExecuteAsync()
        {
            if (await CheckUser(CurrentUser))
            {
                await http.PutJsonAsync("api/User/Edit", CurrentUser);
            }
            else
            {
                await http.PostJsonAsync("api/User/Add", CurrentUser);
            }
        }

        public void CurrentObjectToNull()
        {
            CurrentUser = null;
        }
    }
}
