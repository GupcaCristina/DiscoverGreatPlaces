using AutoMapper;
using Microsoft.AspNetCore.Http;
using Places.BLL.Interfaces;
using Places.DAL.Interfaces;
using Places.DAL.Iterfaces;
using Places.Domain;
using Places.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Places.BLL.Services
{
    public class ImageServices :IImageServices
    {
        IFileRepository _fileRepository;
        private IRepository<Image> _repository;
        public ImageServices(IRepository<Image> repository , IFileRepository fileRepository)
        {
            _repository = repository;
            _fileRepository = fileRepository;
        }

      
        public void SaveImage(ImageDTO imageDTO)
        {
            var newImage = Mapper.Map<Image>(imageDTO);
            _repository.Add(newImage);
            _repository.SaveChanges();        

        }

        public List<ImageDTO> GetImagesById(int id)
        {
            var result = _fileRepository.GetImagesById(id);
            return Mapper.Map<List<ImageDTO>>(result);
        }
        public byte[] GetByteArrayFromImage(IFormFile file)
        {
            using (var target = new MemoryStream())
            {
                file.CopyTo(target);
                return target.ToArray();
            }
        }

        public string ConvertImage(byte[] image)
        {
            var base64 = Convert.ToBase64String(image);
            return  string.Format("data:image/png;base64,{0}", base64);
        }
    }
}
