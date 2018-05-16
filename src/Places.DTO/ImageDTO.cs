using System;
using System.Collections.Generic;
using System.Text;

namespace Places.DTO
{
    public class ImageDTO
    {


        public int Id { get; set; }
        public string Name { get; set; }
        public string ContentType { get; set; }

        public byte[] PlaceImage { get; set; }
        public int PlaceId { get; set; }
       




    }
}
