using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Places.Domain
{
    public abstract class Place :Entity<int>
    {
        public string Name { get; set; }
        public string Descriprion { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }


        [ForeignKey("WorkSchedule")]
        public int IdWorkSchedule { get; set; }
        public WorkSchedule WorkSchedule { get; set; }

        [ForeignKey("Address")]
        public int IdAddress { get; set; }
        public Address Address{ get; set; }
        public string Images { get; set; }

        [ForeignKey("Menu")]
        public int IdMenu { get; set; }
        public Menu Menu { get; set; }


        public List<Review> Reviews { get; set; }= new List<Review>();
        public  List<Facilitie> Facilities { get; set; } = new List<Facilitie>();





    }
}
