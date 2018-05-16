using System;
using System.Collections.Generic;
using System.Text;

namespace Places.Domain
{
    public class PlaceType:Entity<int>
    {
        public string Name { get; set; }
    }
}
