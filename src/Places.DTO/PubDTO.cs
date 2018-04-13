using System;
using System.Collections.Generic;
using System.Text;

namespace Places.DTO
{
    public class PubDTO
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Descriprion { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public int IdWorkSchedule { get; set; }
        public int IdAddress { get; set; }
        public string Images { get; set; }

        public List<MenuDTO> Menus { get; set; }
        public List<ReviewDTO> Reviews { get; set; } = new List<ReviewDTO>();
        public List<FacilitieDTO> Facilities { get; set; } = new List<FacilitieDTO>();
    }
}
