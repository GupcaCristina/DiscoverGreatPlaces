using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using DomainUtil;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NLog;
using Places.BLL.Interfaces;

using Places.Domain;
using Places.DTO;
using Places.Web.Models;
using Places.Web.Models.ViewModels;
using LogLevel = NLog.LogLevel;

namespace Places.Web.Controllers
{
    public class PlacesController : Controller
    {
        private IPlaceServices _placesService;
        private IAddressServices _addressService;
        private IImageServices _imageServices;
        private IFacilitiesServices _facilitiesServices;
        private Logger _logger;
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        public IMapper Mapper  { get; set; }
      



        public PlacesController(
                            IPlaceServices placeServices,
                            IAddressServices addressService,
                            IFacilitiesServices facilitiesServices,
                            IImageServices imageServices,  
                            IMapper mapper,
                        
            UserManager<ApplicationUser> userManager = null,
            SignInManager<ApplicationUser> signInManager = null)
        {
            _placesService = placeServices;
            _addressService = addressService;
            _facilitiesServices = facilitiesServices;
            _userManager = userManager;
            _imageServices = imageServices;
            _signInManager = signInManager;
            _logger = LogManager.GetCurrentClassLogger();
            Mapper = mapper;
        }

        public ActionResult Index(int page, int? placeType, string searchString)
        {      
           
            var pageSize = 6;
            var places = _placesService.GetPaginatedList(pageSize, placeType,  searchString, page);
          
            var allPlaces = Mapper.Map<PaginatedList<PlaceViewModel>>(places);
           
            allPlaces.PageIndex = places.PageIndex;
            allPlaces.TotalPages = places.TotalPages;

            for (int i = 0; i < places.Count; i++)
            {
                if (places[i].Images.Count != 0)
                    allPlaces[i].Image = _imageServices.ConvertImage(places[i].Images[0].PlaceImage);

            }
            return View(allPlaces);
        }
      
        public IActionResult Details(int id)
        {
            if (id <= 0)
            {
                _logger.Log(LogLevel.Error, $"Place with id={id} doesn`t exist");
                return View("Error", new ErrorViewModel() { ErrorMessage = "The place that you are looking for doesn`t exist." });
            }

            PlaceDetailsDTO place;
            try
            {
                 place = _placesService.GetPlaceDetails(id);
            }
            catch (NullReferenceException ex)
            {
                _logger.Log(LogLevel.Warn, $"Place with id={id} was not found !");
                return View("Error", new ErrorViewModel() { ErrorMessage = $"Place with id={id} was not found!!!" });
            }
            var placeDetails = Mapper.Map<PlaceDetailsViewModel>(place);
            if (placeDetails!=null && placeDetails.Images.Count != 0 )
            {
                byte[] imagebyte = placeDetails.Images[0].PlaceImage;
                ViewBag.imagesrc = _imageServices.ConvertImage(imagebyte);
            }
            return View(placeDetails);

           
        }

        [AuthorizeAttribute]
        public IActionResult Create()
        {
            var countries = _addressService.GetCountries();
            var placeTypes =  _placesService.GetTypes();
            var user = _userManager.GetUserId(User);
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
                newPlace.Images = _imageServices.GetImages(img);
            }         
            if (ModelState.IsValid)
            {               
                _placesService.SavePlace(newPlace);
                return RedirectToAction(nameof(GetPlacesByUser), new { page = 1 });
            }
            _logger.Log(LogLevel.Warn, "Model passed by user is not valid");
            return View("Error", new ErrorViewModel() { ErrorMessage = $"The information in the fields is not valid . Please try again" });

        }

        [AuthorizeAttribute]
        public IActionResult Edit(int id)
        {
            CreatePlaceDTO place; 
            try
            {
                place= _placesService.GetById(id);

            }
            catch (NullReferenceException e)
            {
                _logger.Log(LogLevel.Error, $"Place with id={id} doesn`t exist");
                return View("Error", new ErrorViewModel() { ErrorMessage = "The place that you are looking for doesn`t exist." });
            }
       
         
            var placeModel = Mapper.Map<CreatePlaceViewModel>(place);
            if (place == null)
            {
                _logger.Log(LogLevel.Error, $"Place with id={id} requested for edit was not found");
                return View("Error", new ErrorViewModel() { ErrorMessage = "The place that you are looking for was not found ." });
            }

            ViewBag.UserId = _userManager.GetUserId(User);
            ViewBag.Countries = _addressService.GetCountries();
            ViewBag.Cities = _addressService.GetCities(placeModel.CountryId);
            ViewBag.Streets = _addressService.GetStreets(placeModel.CountryId, placeModel.CityId);
            ViewBag.Types = _placesService.GetTypes();
            return View(placeModel);
        }

        [AuthorizeAttribute]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, CreatePlaceViewModel placeViewModel, List<IFormFile> img)
        {
            if (id != placeViewModel.Id)
            {
                _logger.Log(LogLevel.Error, "Place requested for update was not found");
                return View("Error", new ErrorViewModel() { ErrorMessage = $"The place that you whant to edit for was not found ." });
            }
            if (ModelState.IsValid)
            {
                try
                {
                    var placeDTO = Mapper.Map<CreatePlaceDTO>(placeViewModel);
                    if (img != null)
                    {
                        placeDTO.Images = _imageServices.GetImages(img);
                    }
                    _placesService.UpdatePlace(placeDTO);
                }
                catch (DbUpdateConcurrencyException)
                {
                    _logger.Log(LogLevel.Error,"Update doesn`t succed");
                    return View("Error", new ErrorViewModel() { ErrorMessage = $"Update doesn`t succed . Please try again" });
                }
                return RedirectToAction(nameof(Details), new { id = id });
            }
            _logger.Log(LogLevel.Warn, "Requested Model state is not valid ");
            return View("Error", new ErrorViewModel() { ErrorMessage = $"The information in the fields is not valid . Please try again" });
        }
       

        public IActionResult Delete(int id)
        {
            CreatePlaceDTO placeToDelete;
            try
            {
                placeToDelete = _placesService.GetById(id);
            }
            catch (NullReferenceException ex)
            {
                _logger.Log(LogLevel.Warn, $"Place with id={id} was not found !");
                return View("Error", new ErrorViewModel() { ErrorMessage = "The place that you are looking for was not found .." });
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
           
        
            var places = _placesService.GetPlacesByUser ( userId);
            var allPlaces = Mapper.Map<List<PlaceViewModel>>(places);
          

            for (int i = 0; i < places.Count; i++)
            {
                if (places[i].Images.Count!=0)
                allPlaces[i].Image = _imageServices.ConvertImage(places[i].Images[0].PlaceImage);
            }

            return View(allPlaces);
        }
        #endregion
    }
}