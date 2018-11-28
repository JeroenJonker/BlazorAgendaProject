using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorAgenda.Server.DataAccess;
using BlazorAgenda.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlazorAgenda.Server.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        UserDataAccessLayer UserAccess = new UserDataAccessLayer();

        [HttpGet("[action]")]
        public List<User> GetUsers()
        {
            return UserAccess.GetAllUsers();
        }

        //[HttpGet("[action")]
        //public bool IsUserValid(string password)
        //{
        //    List<User> users = GetUsers();
            
        //}
    }
}