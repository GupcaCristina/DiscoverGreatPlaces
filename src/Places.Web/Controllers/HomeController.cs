using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Places.BLL.Interfaces;
using Places.Web.Models;
using AutoMapper;
using Places.Web.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Places.Domain;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Http;

namespace Places.Web.Controllers
{
    public class HomeController : Controller
    {
        private IPlaceServices _placesService;
        private UserManager<ApplicationUser> _userManager;
        private IImageServices _imageServices;
        public IMapper Mapper { get; set; }


        public HomeController(UserManager<ApplicationUser> userManager,
            IPlaceServices placeServices,
            IImageServices imageServices ,
            IMapper mapper
           )
        {
            _userManager = userManager;
            _placesService = placeServices;
            _imageServices = imageServices;
            Mapper = mapper;
        }

        public IActionResult Index()
        {
            var topPlaces = _placesService.GetTopPlaces(3).ToList();
            var placeTypes = _placesService.GetTypes();
            var places= Mapper.Map<List<PlaceViewModel>>(topPlaces);
            for (int i = 0; i < topPlaces.Count; i++)
            {
                if (topPlaces[i].Images.Count!=0)
                {
                places[i].Image =  _imageServices.ConvertImage(topPlaces[i].Images[0].PlaceImage);

                }
            }
            ViewData["TopPlaces"] = places;
            ViewData["PlaceTypes"] = Mapper.Map<List<LookupViewModel>>(placeTypes);
            ViewData["CurrentCulture"] = System.Threading.Thread.CurrentThread.CurrentCulture;
            ViewData["CurrentUICulture"] = System.Threading.Thread.CurrentThread.CurrentUICulture;

            return View();
        }

        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );
            return LocalRedirect(returnUrl);
        }



        public IActionResult Error()
        {
            return View(new ErrorViewModel( ));
        }
    }
}
