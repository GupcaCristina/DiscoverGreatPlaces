using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Places.Domain
{
    public class Review :Entity<int>
    {
        public DateTime Date { get; set; }
        [Range(0, 10)]
        public int Rating { get; set; }
        public string Description { get; set; }
        public int PlaceId { get; set; }
        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }









    }
}
