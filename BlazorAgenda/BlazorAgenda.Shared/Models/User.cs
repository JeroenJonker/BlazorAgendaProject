using BlazorAgenda.Shared.Interfaces;
using System.Collections.Generic;

namespace BlazorAgenda.Shared.Models
{
    public partial class User : IUser
    {
        public User()
        {
            Event = new HashSet<Event>();
        }

        public int Id { get; set; }
        public string Emailadress { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Password { get; set; }
        public bool Isadmin { get; set; }

        public ICollection<Event> Event { get; set; }
    }
}
