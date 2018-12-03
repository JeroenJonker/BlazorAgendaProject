using BlazorAgenda.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAgenda.Services.Interfaces
{
    public interface IUserService
    {
        User CurrentUser { get; set; }
        Task<bool> AddUser();
        Task<bool> CheckUser();
        byte[] ConvertStringToHash(string text);
    }
}
