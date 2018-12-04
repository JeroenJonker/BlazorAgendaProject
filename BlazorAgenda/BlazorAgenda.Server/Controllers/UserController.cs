using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorAgenda.Server.DataAccess;
using BlazorAgenda.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;

namespace BlazorAgenda.Server.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        UserDataAccessLayer UserAccess = new UserDataAccessLayer();

        [HttpGet("[action]")]
        public List<User> GetAllUsers()
        {
            return UserAccess.GetAllUsers();
        }

        [HttpPost("[action]")]
        public bool IsValidUser([FromBody] User loginuser)
        {
            User dbUser = UserAccess.GetUserByEmail(loginuser.Emailadress);
            if (dbUser != null && dbUser.Password.SequenceEqual(loginuser.Password) &&
                new MailAddress(loginuser.Emailadress).Address == loginuser.Emailadress)
            {
                return true;
            }
            return false;
        }

        [HttpPost("[action]")]
        public bool Add([FromBody]User newuser)
        {
            if (UserAccess.GetUserByEmail(newuser.Emailadress) == null &&
                new MailAddress(newuser.Emailadress).Address == newuser.Emailadress)
            {
                UserAccess.AddUser(newuser);
                return true;
            }
            return false;
        }

        [HttpPut("[action]")]
        public void Edit([FromBody]User user)
        {
            if (new MailAddress(user.Emailadress).Address == user.Emailadress)
            {
                UserAccess.UpdateUser(user);
            }
        }
    }
}