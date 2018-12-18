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

        [HttpGet("[action]/{id}")]
        public IActionResult GetById(int id)
        {
            Event theEvent = EventAccess.GetEvent(id);
            if (theEvent != null)
            {
                return Ok(theEvent);
            }
            return NotFound();
        }

        [HttpPost("[action]")]
        public IActionResult Add([FromBody] Event newEvent)
        {
            if (EventAccess.TryAddEvent(newEvent))
            {
                return CreatedAtAction(nameof(GetById), new { id = newEvent.Id }, newEvent);
            }
            return BadRequest();
        }

        [HttpGet("[action]/{userid}")]
        public IActionResult GetUserEvents(int userid)
        {
            return Ok(EventAccess.GetUserEvents(userid));
        }

        [HttpPut("[action]")]
        public IActionResult Edit([FromBody] Event updateEvent)
        {
            if (EventAccess.TryUpdateEvent(updateEvent))
            {
                return Ok(updateEvent);
            }
            return BadRequest();
        }

        [HttpDelete("[action]")]
        public IActionResult Delete([FromBody] Event deleteEvent)
        {
            if (EventAccess.TryDeleteEvent(deleteEvent))
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}