using System;
using System.Collections.Generic;
using System.Text;

namespace Places.DTO
{
    public class ReviewDTO
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int ServiceRating { get; set; }
        public int KitchenRating { get; set; }
        public int AmbianceRating { get; set; }
        public string Description { get; set; }

        public int UserId { get; set; }

        public int LocationId { get; set; }

    }
}
