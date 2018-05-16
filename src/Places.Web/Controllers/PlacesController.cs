using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DomainUtil;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Places.BLL.Interfaces;

using Places.Domain;
using Places.DTO;
using Places.Web.Models;
using Places.Web.Models.ViewModels;

namespace Places.Web.Controllers
{
    public class PlacesController : Controller
    {
        private IPlaceServices _placesService;
        private IAddressServices _addressService;
        private IImageServices _imageServices;
        private IFacilitiesServices _facilitiesServices;
        private readonly ILogger _logger;
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
      



        public PlacesController(UserManager<ApplicationUser> userManager,
                            IPlaceServices placeServices,
                            IAddressServices addressService,
                            IFacilitiesServices facilitiesServices,
                            IImageServices imageServices,
                            SignInManager<ApplicationUser> signInManager,
                            ILogger<PlacesController> logger  )
        {
            _placesService = placeServices;
            _addressService = addressService;
            _facilitiesServices = facilitiesServices;
            _userManager = userManager;
            _imageServices = imageServices;
            _signInManager = signInManager;
            _logger = logger;
          

        }

        public ActionResult Index(int page, int? placeType, string searchString)
        {
       
            _logger.LogInformation("Getting all place");
            var pageSize = 6;
            var places = _placesService.GetPaginatedList(pageSize, placeType,  searchString, page);

            var allPlaces = Mapper.Map<PaginatedList<PlaceViewModel>>(places);
            allPlaces.PageIndex = places.PageIndex;
            allPlaces.TotalPages = places.TotalPages;

            for (int i = 0; i < places.Count; i++)
            {
                allPlaces[i].Image = _imageServices.ConvertImage(places[i].Images[0].PlaceImage);                           
            }
            return View(allPlaces);
        }
      
        public IActionResult Details(int id)
        {
            PlaceDetailsDTO place;
            try
            {
                var userId = _userManager.GetUserAsync(HttpContext.User).Result.Id;
                ViewBag.UserId = userId;
            }
            catch 
            {
                _logger.LogWarning("User is not signed-in");
                return View("Error",new ErrorViewModel(){ErrorMessage = "You are not signed-in" });
            }           
            try
            {
              place = _placesService.GetPlaceDetails(id);
            }
            catch (NullReferenceException  ex)
            {
                _logger.LogError($"Place with id={id} was not found");
                return View("Error", new ErrorViewModel() { ErrorMessage = $"Place with id={id} was not found" });
            }        
            var placeDetails = Mapper.Map<PlaceDetailsViewModel>(place);

            if (placeDetails == null)
            {
               
                return NotFound();
            }
            byte[] imagebyte = placeDetails.Images[0].PlaceImage;
            var base64 = Convert.ToBase64String(imagebyte);
            ViewBag.imagesrc = string.Format("data:image/png;base64,{0}", base64);
            return View(placeDetails);
        }

        [AuthorizeAttribute]
        public IActionResult Create()
        {
            var countries = _addressService.GetCountries();
            var placeTypes =  _placesService.GetTypes();
            var user = _userManager.GetUserAsync(HttpContext.User).Result.Id;
            var facilities = _facilitiesServices.GetFacilities();

            ViewBag.UserId = user;
            ViewBag.Countries = countries;
            ViewBag.Types = placeTypes;
            ViewBag.Facilities = facilities;

            return View();
        }

        [AuthorizeAttribute]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreatePlaceViewModel place, List<IFormFile> img)
        {
            var newPlace = Mapper.Map<CreatePlaceDTO>(place);
            if (img != null)
            {
                place.Image = new List<ImageViewModel>();
                for (int i = 0; i < img.Count; i++)
                {
                    var newImage = new ImageViewModel();
                    newImage.PlaceImage = _imageServices.GetByteArrayFromImage(img[i]);
                    newImage.Name = System.IO.Path.GetFileName(img[i].FileName);
                    newImage.ContentType = img[i].ContentType;
                   place.Image.Add(newImage);
                }

                newPlace.Images = Mapper.Map<List<ImageDTO>>(place.Image);
            }
          
            if (ModelState.IsValid)
            {               
                _placesService.SavePlace(newPlace);
                return RedirectToAction(nameof(GetPlacesByUser), new { page = 1 });
            }
            _logger.LogWarning("Model passed by user is not valid");

            return View("Index",new { page = 1});
        }

        [AuthorizeAttribute]
        public IActionResult Edit(int id)
        {
            var place = _placesService.GetById(id);
            var placeModel = Mapper.Map<CreatePlaceViewModel>(place);
            if (place == null)
            {
                _logger.LogError("Place requested for edit was not found");
                return NotFound();
            }
            
            var countries = _addressService.GetCountries();
            var cities = _addressService.GetCities(placeModel.CountryId);
            var streets = _addressService.GetStreets(placeModel.CountryId, placeModel.CityId);
            var placeTypes = _placesService.GetTypes();
            var userId = _userManager.GetUserAsync(HttpContext.User).Result.Id;

            ViewBag.UserId = userId;
            ViewBag.Countries = countries;
            ViewBag.Cities = cities;
            ViewBag.Streets = streets;
            ViewBag.Types = placeTypes;
            return View(placeModel);
        }

        [AuthorizeAttribute]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, CreatePlaceViewModel placeViewModel)
        {
            if (id != placeViewModel.Id)
            {
                _logger.LogError("Place requested for update was not found");
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    var placeDTO = Mapper.Map<CreatePlaceDTO>(placeViewModel);
                    _placesService.UpdatePlace(placeDTO);
                }
                catch (DbUpdateConcurrencyException)
                {
                    _logger.LogError("Update doesn`t succed");
                    throw;
                }
                return RedirectToAction(nameof(Details), new { id = id });
            }
            _logger.LogWarning("Requested Model state is not valid ");

            return View(placeViewModel);
        }
       

        public IActionResult Delete(int id)
        {
            if (_placesService.GetById(id)  == null)
            {
                _logger.LogError("Place requested for delete was not found");
                return NotFound();
            }

            _placesService.DeletePlace(id);

            return RedirectToAction(nameof(Index));
        }

        #region
        [AuthorizeAttribute]
        public ActionResult GetPlacesByUser(int page,  string searchString)
        {
            var userId = _userManager.GetUserAsync(HttpContext.User).Result.Id;
            if (_signInManager.IsSignedIn(User))
            {
                ViewData["UserId"] = userId;

            }
           
            int pageSize = 6;
            var places = _placesService.GetPlacesByUser (pageSize, userId, searchString, page);
            var allPlaces = Mapper.Map<PaginatedList<PlaceViewModel>>(places);
            allPlaces.PageIndex = places.PageIndex;
            allPlaces.TotalPages = places.TotalPages;

            for (int i = 0; i < places.Count; i++)
            {
                allPlaces[i].Image = _imageServices.ConvertImage(places[i].Images[0].PlaceImage);
            }

            return View(allPlaces);
        }
        #endregion
    }
}