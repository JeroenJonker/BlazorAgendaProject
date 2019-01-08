using BlazorAgenda.Shared.Models;
using System.Collections.Generic;
using System.Linq;

namespace BlazorAgenda.Server.DataAccess
{
    public class EventDataAccessLayer
    {
        AgendaDBContext db = new AgendaDBContext();

        public Event GetEvent(int id)
        {
            List<Event> events = db.Event.Where(g => g.Id == id).ToList();
            return events.Count > 0 ? events[0] : null;
        }

        public List<Event> GetUserEvents(int userid)
        {
            return db.Event.Where(g => g.Userid == userid).ToList();
        }
 
        public bool TryAddEvent(Event newevent)
        {
            try
            {
                db.Event.Add(newevent);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    
        public bool TryUpdateEvent(Event updatedEvent)
        {
            try
            {
                db.Entry(updatedEvent).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool TryDeleteEvent(Event deletedEvent)
        {
            try
            {
                db.Event.Remove(deletedEvent);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
