using Places.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Places.BLL.Interfaces
{
    public interface IAddressServices
    {
        List<CityDTO> GetCities();
        List<CityDTO> GetCities(int country);
        List<StreetDTO> GetStreets(int countryId , int cityId);


        List<CountryDTO> GetCountries();
    }
}
