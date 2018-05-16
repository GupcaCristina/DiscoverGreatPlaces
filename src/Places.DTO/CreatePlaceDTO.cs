using System;
using System.Collections.Generic;
using System.Text;

namespace Places.DTO
{
    public class CreatePlaceDTO
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Descriprion { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string Website { get; set; }
        public int Type { get; set; }
        public List<ImageDTO> Images { get; set; }
        public string IdUser { get; set; }

        //Address
        public AddressDTO Address { get; set; }
        public List<WorkScheduleDTO> WorkSchedules { get; set; } = new List<WorkScheduleDTO>();
        public List<FacilitieDTO> Facilities { get; set; } = new List<FacilitieDTO>();
        public List<ReviewDTO> Reviews { get; set; } = new List<ReviewDTO>();

    }
}
