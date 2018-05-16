using System;
using System.Collections.Generic;
using System.Text;

namespace Places.Domain
{
   

        interface IEntity<TId>
        {
            TId Id { get; set; }
        }
    


}
