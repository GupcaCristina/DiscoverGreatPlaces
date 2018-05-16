using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Places.Web.Models.ViewModels
{
    public class PlaceViewModel
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
        [EmailAddress]
        public string Website { get; set; }
        public AddressViewModel Address { get; set; }
        [Required]
        public LookupViewModel Type { get; set; }
        public string Image { get; set; }
        public Guid? ImageID { get; set; }
        [Display(Name="Number of Reviews")]
        public int? NumberOfReviews { get; set; }
        [Display(Name="Rating")]
        public float? AvgRating { get; set; }
    }
}
