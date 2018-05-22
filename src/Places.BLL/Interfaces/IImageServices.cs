
using Microsoft.AspNetCore.Http;
using Places.DTO;
using System.Collections.Generic;

namespace Places.BLL.Interfaces
{
    public interface IImageServices
    {
        List<ImageDTO> GetImagesById(int id);
        void SaveImage(ImageDTO imageDTO);
        byte[] GetByteArrayFromImage(IFormFile file);
        string ConvertImage(byte[] image);
        List<ImageDTO> GetImages(List<IFormFile> img);

    }
}
