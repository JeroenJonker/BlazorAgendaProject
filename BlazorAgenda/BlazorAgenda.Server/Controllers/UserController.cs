using System;
using System.Collections.Generic;
using System.Linq;
using BlazorAgenda.Server.DataAccess;
using BlazorAgenda.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;

namespace BlazorAgenda.Server.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller, IObjectController<User>
    {
        UserDataAccessLayer UserAccess = new UserDataAccessLayer();

        [HttpPost("[action]")]
        public IActionResult Add([FromBody]User newuser)
        {
            newuser.Password = ConvertStringToHash(newuser.Password);
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
            updateUser.Password = ConvertStringToHash(updateUser.Password);
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
            loginuser.Password = ConvertStringToHash(loginuser.Password);
            User dbUser = UserAccess.GetUserByEmail(loginuser.Emailadress);
            if (dbUser != null && loginuser.Emailadress!= null && dbUser.Password.SequenceEqual(loginuser.Password) &&
                new MailAddress(loginuser.Emailadress).Address == loginuser.Emailadress)
            {
                dbUser.Password = "";
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
