using System;
using System.Collections.Generic;
using System.Text;

namespace Places.DTO
{
   public  class PlaceDetailsDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Descriprion { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string Website { get; set; }
        public  AddressDTO Address { get; set; }
        public PlaceTypeDTO Type { get; set; }
       
        public int NumberOfReviews { get; set; }
        public  double RatingAVG { get; set; }
        public Guid UserId { get; set; }

        public List<ImageDTO> Images { get; set; } = new List<ImageDTO>();

        public List<ReviewDTO> Reviews { get; set; } = new List<ReviewDTO>();
        public List<WorkScheduleDTO> WorkSchedules { get; set; } = new List<WorkScheduleDTO>();
        public List<FacilitiesDTO> Facilities { get; set; } = new List<FacilitiesDTO>();

    }
}
