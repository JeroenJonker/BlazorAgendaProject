using BlazorAgenda.Server.DataAccess;
using BlazorAgenda.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlazorAgenda.Server.Controllers
{
    [Route("api/[controller]")]
    public class EventController : Controller, IObjectController<Event>
    {
        EventDataAccessLayer EventAccess = new EventDataAccessLayer();

        [HttpPost("[action]")]
        public IActionResult Add([FromBody] Event newEvent)
        {
            if (EventAccess.TryAddEvent(newEvent))
            {
                return CreatedAtAction(nameof(GetObjectById), new { id = newEvent.Id }, newEvent);
            }
            return BadRequest();
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

        private IActionResult GetObjectById(int id)
        {
            if (EventAccess.GetEvent(id) is Event idEvent)
            {
                return Ok(idEvent);
            }
            return NotFound();
        }

        [HttpGet("[action]/{userid}")]
        public IActionResult GetUserEvents(int userid)
        {
            return Ok(EventAccess.GetUserEvents(userid));
        }
    }
}
