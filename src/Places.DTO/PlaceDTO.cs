using Places.Domain;
using System;
using System.Collections.Generic;
using System.Text;


namespace Places.DTO
{
    public class PlaceDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Descriprion { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string Website { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public PlaceTypeDTO Type { get; set; }


        //Address
        public AddressDTO Address { get; set; }
        public List<Image> Images { get; set; } = new List<Image>();
        public Guid? ImageId { get; set; }
       
        public int? NumberOfReviews { get; set; }
    
        public double? AvgRating { get; set; }
     

    }
}
