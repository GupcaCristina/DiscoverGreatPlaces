using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Places.Web.Models.ViewModels
{
    public class PlaceDetailsViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [Phone]
        public string Telephone { get; set; }
        public string Website { get; set; }
        [Required]
        public String Descriprion { get; set; }
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public AddressViewModel Address { get; set; }
        [Required]
        public LookupViewModel Type { get; set; }

        [Display(Name = "Number of Reviews")]
        public int? NumberOfReviews { get; set; }
        [Display(Name = "Rating")]
        public float? RatingAVG { get; set; }

        public List<ImageViewModel> Images { get; set; } = new List<ImageViewModel>();
        public List<WorkScheduleViewModel> WorkSchedules { get; set; } = new List<WorkScheduleViewModel>();
        public List<LookupViewModel> Facilities { get; set; } = new List<LookupViewModel>();
        public List<ReviewViewModel> Reviews { get; set; } = new List<ReviewViewModel>();

    }
}
