
using Microsoft.AspNetCore.Http;
using Places.Domain;
using Places.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Places.BLL.Interfaces
{
    public interface IImageServices
    {
        List<ImageDTO> GetImagesById(int id);
        void SaveImage(ImageDTO imageDTO);
        byte[] GetByteArrayFromImage(IFormFile file);
        string ConvertImage(byte[] image);

    }
}
