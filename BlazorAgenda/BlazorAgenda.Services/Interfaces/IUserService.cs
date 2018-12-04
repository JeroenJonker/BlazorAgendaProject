﻿using BlazorAgenda.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAgenda.Services.Interfaces
{
    public interface IUserService : IDefaultObjectService
    {
        User CurrentUser { get; set; }
        Task<bool> CheckUser();
        Task<List<User>> GetContacts();
        Task<List<Event>> GetEvents();
        string ConvertStringToHash(string text);
    }
}