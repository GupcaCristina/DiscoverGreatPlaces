using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Places.Web.Models.ViewModels
{

    public class ReviewViewModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        [Required]
        [Range(0, 10)]
        public int Rating { get; set; }
        [Required]
        [StringLength(300)]
        public string Description { get; set; }
        public  string ApplicationUserId { get; set; }
        public String UserName{ get; set; }
        [Required]
        public int PlaceId { get; set; }
    }
}
