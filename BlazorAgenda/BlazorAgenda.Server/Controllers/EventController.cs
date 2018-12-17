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

        [HttpPost("[action]")]
        public void Add([FromBody] Event newEvent)
        {
            EventAccess.AddEvent(newEvent);
        }

        [HttpGet("[action]/{userid}")]
        public List<Event> GetUserEvents(int userid)
        {
            return EventAccess.GetUserEvents(userid);
        }

        [HttpPut("[action]")]
        public void Edit([FromBody] Event newEvent)
        {
            EventAccess.UpdateEvent(newEvent);
        }

        [HttpDelete("[action]")]
        public void Delete([FromBody] Event deleteEvent)
        {
            EventAccess.DeleteEvent(deleteEvent);
        }
    }
}