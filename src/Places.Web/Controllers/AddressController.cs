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
using Places.DTO;
using Places.Web.Models;

namespace Places.Web.Controllers
{
    public class AddressController:Controller
    {
        private  IAddressServices _addressService;

        public AddressController(IAddressServices addressService)
        {
            _addressService = addressService;
        }

        public  List<CountryDTO> GetCountries()
        {         
            var countries = _addressService.GetCountries();
            return countries;

        }
        [HttpGet]
        public JsonResult GetCities(int id)
        {
            List<CityDTO> cities;
            try
            {
                cities = _addressService.GetCities(id);
            }
            catch (NullReferenceException ex)
            {              
                return Json("None City was found!!!" );
            }
            var city = JsonConvert.SerializeObject(cities);

            return Json(city);
        }

        [HttpGet]
        public JsonResult GetStreets(int countryId , int cityId)

        {
            List<StreetDTO> streets;
            try
            {
                streets = _addressService.GetStreets(countryId, cityId);
            }
            catch (NullReferenceException ex)
            {
                return Json("None Street was found!!!");
            }
              
            var street = JsonConvert.SerializeObject(streets);
            return Json(street);
        }

    }
}
