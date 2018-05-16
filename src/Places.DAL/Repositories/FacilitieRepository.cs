using Places.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;


namespace Places.DAL.Repositories
{
    class FacilitieRepository:IRepository<Facilitie>
    {
        public FacilitieRepository(DbContext context) : base(context)
        {

        }
    }
}
