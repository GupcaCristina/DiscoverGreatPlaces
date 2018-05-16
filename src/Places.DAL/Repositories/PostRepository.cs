using Places.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;


namespace Places.DAL.Repositories
{ 
    public class PostRepository :IRepository<Post>
    {
        public PostRepository(DbContext context) : base(context)
        {

        }
    }
}
