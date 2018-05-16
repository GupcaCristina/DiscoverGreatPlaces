using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Places.Web.Models.ViewModels
{
    public class AddressViewModel
    {

        public string Street { get; set; }
        public string StreetNumber { get; set; }
        public string PostalCode { get; set; }

        [Display(Name = "Additional Information")]
        public string AdditionalInfo { get; set; }

      
        public string City { get; set; }


        public string Country { get; set; }
    }
}
