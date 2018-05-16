using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Places.DTO
{
    public class AddressDTO
    {

        public int Id { get; set; }
        public string PostalCode { get; set; }

        public string StreetNumber { get; set; }
        public string AdditionalInfo { get; set; }

        public StreetDTO Street { get; set; }

    }
}
