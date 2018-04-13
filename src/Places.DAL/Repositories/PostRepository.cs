using Places.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;


namespace Places.DAL.Repositories
{ 
    public class PostRepository :Repository<Post>
    {
        public PostRepository(DbContext context) : base(context)
        {

        }
    }
}
