using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Places.Web.Models.ViewModels
{
    public class CreatePlaceViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Descriprion { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [Phone]
        public string Telephone { get; set; }
        public string Website { get; set; }
        [Required]
        public int Type { get; set; }

        public List<ImageViewModel> Image { get; set; } = new List<ImageViewModel>();
        public string UserId { get; set; }

        //Address
        [Required]
        public int Street { get; set; }
        [Required]
        public string StreetNumber { get; set; }
      
        public string PostalCode { get; set; }

        [Display(Name = "Additional Information")]
        public string AdditionalInfo { get; set; }

        [Display(Name = "City")]
        public int CityId { get; set; }

        [Display(Name = "Country")]
        public int CountryId { get; set; }


        public List<WorkScheduleViewModel> WorkSchedule { get; set; } = new List<WorkScheduleViewModel>();
        public List<LookupViewModel> Facilities { get; set; } = new List<LookupViewModel>();
        public List<ReviewViewModel> Reviews { get; set; } = new List<ReviewViewModel>();

    }
}
