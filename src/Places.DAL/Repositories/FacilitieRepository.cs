using Places.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;


namespace Places.DAL.Repositories
{
    class FacilitieRepository:Repository<Facilitie>
    {
        public FacilitieRepository(DbContext context) : base(context)
        {

        }
    }
}
