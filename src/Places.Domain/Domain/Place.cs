
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Places.Domain
{
    public  class Place :Entity<int>
    {
      

        public string Name { get; set; }

        public string Descriprion { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string Website{ get; set; }
        public DateTime AddedDate { get; set; } 
        public DateTime? ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("Type")]
        public int PlaceTypeId { get; set; }
        public PlaceType Type { get; set; }

        [ForeignKey("Address")]
        public int? IdAddress { get; set; }
        public Address Address{ get; set; }

        [ForeignKey("User")]
        public string IdUser { get; set; }
        public ApplicationUser User{ get; set; }
        public List<Image> Images { get; set; } = new List<Image>();
        public List<WorkSchedule> WorkSchedules { get; set; } = new List<WorkSchedule>();
        public List<Review> Reviews { get; set; }= new List<Review>();
        public List<PlaceFacilitie> PlaceFacilities { get; set; } = new List<PlaceFacilitie>();

        //[ForeignKey("Menu")]
        //public int IdMenu { get; set; }
        //public Menu Menu { get; set; }



    }
}
