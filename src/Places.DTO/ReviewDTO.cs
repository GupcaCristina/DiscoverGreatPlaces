using System;
using System.Collections.Generic;
using System.Text;

namespace Places.DTO
{
    public class ReviewDTO
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Rating { get; set; }
        public string Description { get; set; }
        public string ApplicationUserId { get; set; }
        public int PlaceId { get; set; }

    }
}
