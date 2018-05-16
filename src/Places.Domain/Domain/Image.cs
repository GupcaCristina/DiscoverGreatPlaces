using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Places.Domain
{
    public class Image:Entity<int>
    {


        public string Name { get; set; }
        public string ContentType { get; set; }

        public byte[] PlaceImage { get; set; }

        [ForeignKey("Place")]
        public int PlaceId { get; set; }
        public Place Place { get; set; }





    }
}
