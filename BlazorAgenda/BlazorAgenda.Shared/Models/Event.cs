using System;
using System.Collections.Generic;

namespace BlazorAgenda.Shared.Models
{
    public partial class Event
    {
        public int Id { get; set; }
        public string Summary { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Location { get; set; }
        public string Color { get; set; }
        public string Emailadress { get; set; }

        public User EmailadressNavigation { get; set; }
    }
}
