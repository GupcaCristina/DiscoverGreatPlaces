using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Places.Web.Models.ViewModels
{
    public class ImageViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ContentType { get; set; }
        [Required]
        public byte[] PlaceImage { get; set; }
        public int PlaceId { get; set; }
    }
}
