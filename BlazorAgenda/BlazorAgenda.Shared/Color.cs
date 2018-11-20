using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorAgenda.Shared
{
    public class Color : BaseObject
    {
        public string ColorId { get; set; }
        public string Background { get; set; }
        public string Foreground { get; set; }
    }
}
