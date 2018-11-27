using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorAgenda.Shared
{
    public class Contact
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAdress { get; set; }
        public string FullName { get { return string.Format("{0} {1}", FirstName, LastName); } }
        public bool IsCalendarShown { get; set; } = false;
    }
}
