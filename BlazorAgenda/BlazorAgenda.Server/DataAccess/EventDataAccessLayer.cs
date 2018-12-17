using BlazorAgenda.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorAgenda.Server.DataAccess
{
    public class EventDataAccessLayer
    {
        AgendaDBContext db = new AgendaDBContext();

        public List<Event> GetUserEvents(int Userid)
        {
            try
            {
                return db.Event.Where(g => g.Userid == Userid).ToList();
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<Event> GetAllEvents()
        {
            try
            {
                return db.Event.ToList();
            }
            catch
            {
                throw;
            }
        }
 
        public void AddEvent(Event newevent)
        {
            try
            {
                db.Event.Add(newevent);
                db.SaveChanges();
            }
            catch
            {
                throw;
            }
        }
    
        public void UpdateEvent(Event updatedEvent)
        {
            try
            {
                db.Entry(updatedEvent).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public void DeleteEvent(Event deletedEvent)
        {
            try
            {
                db.Event.Remove(deletedEvent);
                db.SaveChanges();
            }
            catch
            {
                throw;
            }
        }
    }
}
