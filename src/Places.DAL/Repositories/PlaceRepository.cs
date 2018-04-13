using Places.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Places.DAL.Interfaces;

namespace Places.DAL.Repositories
{
    public class PlaceRepository : Repository<Place> , IPlaceRepository
    {
        public PlaceRepository(DbContext context):base(context)
        {

        }
    }
}
