using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Places.Domain
{
    public class Address :Entity<int>
    {

        public string PostalCode { get; set; }

        public string StreetNumber { get; set; }
        public string AdditionalInfo { get; set; }

        [ForeignKey("Street")]
        public int StreetID { get; set; }
        public Street Street { get; set; }
    }
}
