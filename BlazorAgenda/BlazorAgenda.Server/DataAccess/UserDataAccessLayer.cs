using BlazorAgenda.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorAgenda.Server.DataAccess
{
    public class UserDataAccessLayer
    {
        AgendaDBContext db = new AgendaDBContext();
   
        public List<User> GetAllUsers()
        {
            try
            {
                return db.User.ToList();
            }
            catch
            {
                throw;
            }
        }

        public User GetUserByEmail(string email)
        {
            return db.User.Find(email);
        }

        public void AddUser(User user)
        {
            try
            {
                db.User.Add(user);
                db.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public void UpdateUser(User user)
        {
            try
            {
                db.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
            }
            catch
            {
                throw;
            }
        }
    }
}
