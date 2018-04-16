using Places.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;


namespace Places.DAL.Repositories
{
    public class ReviewRepository:Repository<Review>
    {
        public ReviewRepository(DbContext context) : base(context)
        {

        }
    }
}
