using System;
using System.Collections.Generic;

namespace BlazorAgenda.Shared.Models
{
    public partial class User
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

        public ICollection<Event> Event { get; set; }
    }
}
