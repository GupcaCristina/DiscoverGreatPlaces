﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Places.Domain
{
    public class Review :Entity<int>
    {
        public DateTime Date { get; set; }
        public int ServiceRating { get; set; }
        public int KitchenRating { get; set; }
        public int AmbianceRating { get; set; }
        public string Description { get; set; }

        public int UserId { get; set; }

        public int LocationId { get; set; }

    }
}
