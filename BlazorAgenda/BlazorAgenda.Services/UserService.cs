using BlazorAgenda.Services.Interfaces;
using BlazorAgenda.Shared.Models;
using BlazorAgenda.Shared.Properties;
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
    public class UserService : DefaultObjectService<User>, IUserService
    {
        public UserService(HttpClient client) : base(client)
        {
        }

        public async Task<User> CheckUser(User user)
        {
            user.Password = ConvertStringToHash(user.Password);
            return await http.PostJsonAsync<User>(Resources.UserApi_IsValidUser, user);
        }

        public async Task<bool> IsUserInUse(User user)
        {
            return await http.PostJsonAsync<bool>(Resources.UserApi_IsUserInUse, user);
        }

        public async Task<List<User>> GetContacts()
        {
            return await http.GetJsonAsync<List<User>>(Resources.UserApi_GetAllUsers);
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
    }
}
