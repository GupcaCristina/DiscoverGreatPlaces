using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Places.Domain
{
   public  class Post :Entity<int>
    {
        public string PostTitle { get; set; }
        public string PostBody { get; set; }
        public DateTime PostDate { get; set; }
        public string Image { get; set; }
        public string Video { get; set; }

        [ForeignKey("Place")]
        public int PlaceId { get; set; }
        public Place Place { get; set; }
       
    }
}
