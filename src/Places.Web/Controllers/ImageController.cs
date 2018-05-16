using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Places.DAL.Iterfaces;
using Places.Web.Models.ViewModels;

namespace Places.Web.Controllers
{
    public class ImageController : Controller
    {
        public byte[] GetByteArrayFromImage(IFormFile file)
        {
            using (var target = new MemoryStream())
            {
                file.CopyTo(target);
                return target.ToArray();
            }
        }

    }

}