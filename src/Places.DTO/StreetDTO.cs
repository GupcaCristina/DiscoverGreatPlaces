using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Places.DTO
{
    public class StreetDTOS
    {

        public int Id { get; set; }
        public string Name { get; set; }

        public int CityID { get; set; }

    }
}
