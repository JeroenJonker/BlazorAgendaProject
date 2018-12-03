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
    public class EventController : Controller
    {
        EventDataAccessLayer EventAccess = new EventDataAccessLayer();

        //[HttpGet("[action]")]
        //public List<Event> GetUsers()
        //{
        //    return UserAccess.GetAllUsers();
        //}
        [HttpPost("[action]")]
        public void Add([FromBody] Event newEvent)
        {
            EventAccess.AddEvent(newEvent);
        }

        [HttpGet("[action]/{email}")]
        public List<Event> GetUserEvents(string email)
        {
            return EventAccess.GetUserEvents(email);
        }

        [HttpPut("[action]")]
        public void Edit([FromBody] Event newEvent)
        {
            EventAccess.UpdateEvent(newEvent);
        }

    }
}