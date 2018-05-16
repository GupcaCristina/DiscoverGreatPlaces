using System;
using System.Collections.Generic;
using System.Text;

namespace Places.Domain
{
   public class PlaceFacilitie
    {
        public int PlaceId { get; set; }
        public Place Place{ get; set; }

        public int FacilitieId { get; set; }
        public Facilitie Facilitie { get; set; }
    }
}
