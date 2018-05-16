using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Places.DTO
{
    public class StreetDTO
    {

        public int Id { get; set; }
        public string Name { get; set; }

        public CityDTO City { get; set; }

    }
}
