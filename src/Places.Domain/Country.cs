using System;
using System.Collections.Generic;
using System.Text;

namespace Places.DTO
{
    public class Country : Entity<int>
    {
        public string Name { get; set; }
    }
}
