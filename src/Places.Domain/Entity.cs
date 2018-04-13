using System;
using System.Collections.Generic;
using System.Text;

namespace Places.DTO
{
    public abstract class Entity<TId>
    {
        public TId Id { get; set; }
    }
}
