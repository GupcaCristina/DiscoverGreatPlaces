using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Places.BLL;
using Places.BLL.Interfaces;
using Places.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Places.Web.Controllers
{
    public class AddressController:Controller
    {
        private  IAddressServices _addressService;

        public AddressController(IAddressServices addressService)
        {
            _addressService = addressService;
        }

        public  List<LookupViewModel> GetCountries()
        {         
            var countries = _addressService.GetCountries();
            var allCountries = Mapper.Map<List<LookupViewModel>>(countries);
            return allCountries;

        }
        [HttpGet]
        public JsonResult GetCities(int id)
        {
            var cities = _addressService.GetCities(id);
            var allcities = Mapper.Map<List<LookupViewModel>>(cities);
            var city = JsonConvert.SerializeObject(allcities);
            return Json(city);
        }

        [HttpGet]
        public ActionResult GetStreets(int countryId , int cityId)

        {
            var streets = _addressService.GetStreets(countryId, cityId);          
            var allStreets = Mapper.Map<List<LookupViewModel>>(streets);
            var street = JsonConvert.SerializeObject(allStreets);
            return Json(street);
        }

    }
}
