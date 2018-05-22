
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Places.BLL.Interfaces;
using Places.DTO;
using Places.Web.Controllers;
using Xunit;

namespace Web.Controllers.Test
{
    public class AddressControllerTest
    {
       private Mock<IAddressServices> _addressServices{ get; set; }
        private AddressController _addressController;
        public AddressControllerTest()
        {
           _addressServices= new Mock<IAddressServices>();
           _addressController= new AddressController(_addressServices.Object);
        }
        [Fact]
        public void GetCountries_ResturnsAListOfAllCountries()
        {
            _addressServices.Setup(s => s.GetCountries())
                .Returns(new List<CountryDTO>()
                {
                    new CountryDTO(){Id = 1,Name = "Moldova"},
                    new CountryDTO(){Id = 2,Name = "Romania"}
                });
           var result= _addressController.GetCountries();

            Assert.IsType<List<CountryDTO>>(result);
            Assert.IsAssignableFrom<List<CountryDTO>>(result);
            Assert.Equal(2,result.Count);
        }

        [Fact]
        public void GetCities_ResturnsAJsonResultWhenIdIsValid()
        {
            _addressServices.Setup(s => s.GetCities())
                .Returns(new List<CityDTO>()
                {
                    new CityDTO(){Id = 1,Name = "Chisinau"},
                    new CityDTO(){Id = 2,Name = "London"}
                });

           var result= _addressController.GetCities(1);
            Assert.IsType<JsonResult>(result);
        }

        [Fact]
        public void GetCities_ResturnsNullReferenceExceptionWhenIdIsZero()
        {
            _addressServices.Setup(s => s.GetCities(0))
                .Throws<NullReferenceException>();

            var result = _addressController.GetCities(0);
            Assert.IsType<JsonResult>(result);
            Assert.Equal("None City was found!!!", result.Value.ToString());
        }

        [Fact]
        public void GetCities_ResturnsNullReferenceExceptionWhenIdIsNegative()
        {
            _addressServices.Setup(s => s.GetCities(-1))
                .Throws<NullReferenceException>();

            var result = _addressController.GetCities(-1);
            Assert.IsType<JsonResult>(result);
            Assert.Equal("None City was found!!!", result.Value.ToString());
        }
        [Fact]
        public void GetCities_ResturnsNullReferenceExceptionWhenIdDoesnExist()
        {
            _addressServices.Setup(s => s.GetCities(Int32.MaxValue))
                .Throws<NullReferenceException>();

            var result = _addressController.GetCities(Int32.MaxValue);
            Assert.IsType<JsonResult>(result);
            Assert.Equal("None City was found!!!", result.Value.ToString());
        }

        [Fact]
        public void GetStreets_ResturnsListOfStreetsWhenParametersAreValid()
        {
            _addressServices.Setup(s => s.GetStreets(1, 1))
                .Returns(new List<StreetDTO>()
                {
                    new StreetDTO() {Id = 1, Name = "Stfan cel Mare"},
                    new StreetDTO() {Id = 1, Name = "Alba Iulia"},
                });

            var result = _addressController.GetStreets(1, 1);
            Assert.IsType<JsonResult>(result);
        }

        [Fact]
        public void GetStreets_ResturnsNullReferenceExceptionWhenParametersValueDoesnExist()
        {
            _addressServices.Setup(s => s.GetStreets(Int32.MaxValue, Int32.MaxValue))
                .Throws<NullReferenceException>();

            var result = _addressController.GetStreets(Int32.MaxValue, Int32.MaxValue);
            Assert.IsType<JsonResult>(result);
            Assert.Equal("None Street was found!!!", result.Value.ToString());

        }
        [Fact]
        public void GetStreets_ResturnsNullReferenceExceptionWhenParametersAreNegative()
        {
            _addressServices.Setup(s => s.GetStreets(-1,-1))
                .Throws<NullReferenceException>();

            var result = _addressController.GetStreets(-1,-1);
            Assert.IsType<JsonResult>(result);
            Assert.Equal("None Street was found!!!", result.Value.ToString());

        }
        [Fact]
        public void GetStreets_ResturnsNullReferenceExceptionWhenParametersAreZero()
        {
            _addressServices.Setup(s => s.GetStreets(0,0))
                .Throws<NullReferenceException>();

            var result = _addressController.GetStreets(0,0);
            Assert.IsType<JsonResult>(result);
            Assert.Equal("None Street was found!!!", result.Value.ToString());

        }
        [Fact]
        public void GetStreets_ResturnsNullReferenceExceptionWhenOneParametersIsInvalid()
        {
            _addressServices.Setup(s => s.GetStreets(-1, 1))
                .Throws<NullReferenceException>();

            var result = _addressController.GetStreets(-1, 1);
            Assert.IsType<JsonResult>(result);
            Assert.Equal("None Street was found!!!", result.Value.ToString());

        }





    }
}
