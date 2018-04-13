using System;
using System.Collections.Generic;
using System.Text;

namespace Places.DTO
{
   public  class PostDTO
    {
        public int Id { get; set; }
        public string PostTitle { get; set; }
        public string PostBody { get; set; }
        public DateTime PostDate { get; set; }
        public string Image { get; set; }
        public string Video { get; set; }
        public int PlaceID { get; set; }
    }
}
