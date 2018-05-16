using Places.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Places.Domain
{
    public class Facilitie:Entity<int>
    {
        public string Name { get; set; }
        public List<PlaceFacilitie> PlaceFacilities { get; set; } = new List<PlaceFacilitie>();
    }
}
