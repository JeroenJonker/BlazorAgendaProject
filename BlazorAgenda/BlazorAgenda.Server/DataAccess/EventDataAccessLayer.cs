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

        public List<Event> GetUserEvents(string email)
        {
            try
            {
                return db.Event.Where(g => g.Emailadress == email).ToList();
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

        //To Update the records of a particluar employee      
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
    }
}
