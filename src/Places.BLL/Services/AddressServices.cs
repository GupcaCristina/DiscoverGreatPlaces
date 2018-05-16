using AutoMapper;
using Places.BLL.Interfaces;
using Places.DAL.Interfaces;
using Places.Domain;
using Places.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Places.BLL
{
    public class AddressServices :IAddressServices
    {
     
      
            private IRepository<City> _cityRepository;
        private IRepository<Country> _countryRepository;

        private IRepository<Street> _streetRepository;


        public AddressServices(IRepository<City> cityRepository ,IRepository<Country> countryRepository , IRepository<Street> streetRepository)
            {

            _cityRepository = cityRepository;
            _countryRepository = countryRepository;
            _streetRepository = streetRepository;
            }
            public List<CityDTO> GetCities()
            {
                var cities = _cityRepository.Get();

                var allCities = Mapper.Map<List<CityDTO>>(cities);

                return allCities;
            }
     

        public List<CountryDTO> GetCountries()
        {
            var countries = _countryRepository.Get();         
            var allCountries = Mapper.Map<List<CountryDTO>>(countries);
            return allCountries;
        }
        public List<CityDTO> GetCities(int country)
        {
            var cities = _cityRepository.Get().Where(p => p.CountryID == country);
            var allCities = Mapper.Map<List<CityDTO>>(cities);

            return allCities;
        }
        public List<StreetDTO> GetStreets(int countryId, int cityId)
        {
            var streets = _streetRepository.Get().Where(p => p.CityID == cityId && p.City.CountryID == countryId);
            var allStreets = Mapper.Map<List<StreetDTO>>(streets);
            return allStreets;

        }



    }
}
