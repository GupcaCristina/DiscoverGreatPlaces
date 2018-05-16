using Places.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;


namespace Places.DAL.Iterfaces
{
    public interface IFileRepository
    {
        List<Image> GetImagesById(int id);
    }
}
