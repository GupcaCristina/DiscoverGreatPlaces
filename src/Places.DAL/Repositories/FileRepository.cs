using Microsoft.EntityFrameworkCore;
using Places.DAL.Iterfaces;
using Places.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Places.DAL.Repositories
{
    public class FileRepository : IRepository<Image>, IFileRepository
    {
        public FileRepository(DbContext context) : base(context)
        {

        }

        public List<Image> GetImagesById(int id)
        {
            var result = Get().Where(p =>p.PlaceId==id).ToList();
            return  result;
        }
      
    }
}
