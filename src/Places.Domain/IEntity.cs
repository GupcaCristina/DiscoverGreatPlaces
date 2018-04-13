using System;
using System.Collections.Generic;
using System.Text;

namespace Places.DTO
{
   

        interface IEntity<TId>
        {
            TId Id { get; set; }
        }
    


}
