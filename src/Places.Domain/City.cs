using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Places.DTO
{
    public class City: Entity<int>
    {
        public string Name { get; set; }
        [ForeignKey("Country")]
        public int CountryID { get; set; }
        public Country Country { get; set; }
    }
}
