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
    public class UserController : Controller, IObjectController<User>
    {
        UserDataAccessLayer UserAccess = new UserDataAccessLayer();

        [HttpPost("[action]")]
        public IActionResult Add([FromBody]User newuser)
        {
            if (UserAccess.GetUserByEmail(newuser.Emailadress) == null &&
                new MailAddress(newuser.Emailadress).Address == newuser.Emailadress &&
                UserAccess.TryAddUser(newuser))
            {
                return CreatedAtAction(nameof(GetObjectById), new { id = newuser.Id }, newuser);
            }
            return BadRequest();
        }

        [HttpPut("[action]")]
        public IActionResult Edit([FromBody]User updateUser)
        {
            if (UserAccess.TryUpdateUser(updateUser))
            {
                return Ok(updateUser);
            }
            return BadRequest();
        }

        [HttpDelete("[action]")]
        public IActionResult Delete([FromBody] User deleteUser)
        {
            if (UserAccess.TryDeleteUser(deleteUser))
            {
                return Ok();
            }
            return BadRequest();
        }

        private IActionResult GetObjectById(int id)
        {
            if (UserAccess.GetUser(id) is User user)
            {
                return Ok(user);
            }
            return NotFound();
        }

        [HttpGet("[action]")]
        public IActionResult GetAllUsers()
        {
            List<User> users = UserAccess.GetAllUsers();
            foreach (User user in users)
            {
                user.Password = "";
            }
            return Ok(users);
        }

        [HttpPost("[action]")]
        public IActionResult IsValidUser([FromBody] User loginuser)
        {
            User dbUser = UserAccess.GetUserByEmail(loginuser.Emailadress);
            if (dbUser != null && loginuser.Emailadress!= null && dbUser.Password.SequenceEqual(loginuser.Password) &&
                new MailAddress(loginuser.Emailadress).Address == loginuser.Emailadress)
            {
                return Ok(dbUser);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost("[action]")]
        public IActionResult IsUserInUse([FromBody] User user)
        {
            if(UserAccess.GetUserByEmail(user.Emailadress) != null)
            {
                return Ok(true);
            }
            return BadRequest();
        }
    }
}