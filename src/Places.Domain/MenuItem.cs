using System;
using System.Collections.Generic;
using System.Text;

namespace Places.Domain
{
    public class MenuItem:Entity<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
    }
}
