using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Places.DTO
{
    public class Street : Entity<int>
    {
    
        
        public string Name { get; set; }

        [ForeignKey("City")]
        public int CityID { get; set; }
        public City City { get; set; }
    }
}
