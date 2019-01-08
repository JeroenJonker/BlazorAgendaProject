using BlazorAgenda.Shared.Models;
using System.Collections.Generic;
using System.Linq;

namespace BlazorAgenda.Server.DataAccess
{
    public class UserDataAccessLayer
    {
        AgendaDBContext db = new AgendaDBContext();

        public User GetUser(int id)
        {
            List<User> users = db.User.Where(g => g.Id == id).ToList();
            return users.Count > 0 ? users[0] : null;
        }

        public List<User> GetAllUsers()
        {
            return db.User.ToList();
        }

        public User GetUserByEmail(string email)
        {
            List<User> users = db.User.Where(g => g.Emailadress == email).ToList();
            return users.Count > 0 ? users[0] : null;
        }

        public bool TryAddUser(User user)
        {
            try
            {
                db.User.Add(user);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool TryUpdateUser(User user)
        {
            try
            {
                db.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool TryDeleteUser(User user)
        {
            try
            {
                db.User.Remove(user);
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
