using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Places.Web.Models.ViewModels
{
    public class SendMessageViewModel
    {
        [Required]
        public int PlaceId { get; set; }
        [Required]
        [EmailAddress]
        public string From { get; set; }
        [Required]
        [EmailAddress]
        public string To { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        public string Body { get; set; }
    }
}
