using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit;
using Microsoft.Extensions.Logging;
using Places.BLL.Interfaces;
using Places.Domain;
using Microsoft.AspNetCore.Identity;
using Moq;
using Places.Web.Controllers;
using System.Threading.Tasks;
using AutoMapper;
using Places.Web.Models.ViewModels;
using DomainUtil;
using Microsoft.AspNetCore.Mvc;
using Places.DTO;
using Places.BLL.Profiles;
using Places.Web.Models.Profiles;

namespace Places.Test
{
    public class PlacesControllerTest
    {
        private Mock<IPlaceServices> _placesService;
        private Mock<IAddressServices> _addressService;
        private Mock<IImageServices> _imageServices;
        private Mock<IFacilitiesServices> _facilitiesServices;
        private Mock<ILogger<PlacesController>> _logger;
        private Mock<UserManager<ApplicationUser>> _userManager;
        private Mock<SignInManager<ApplicationUser>> _signInManager;
        private PlacesController _placesController;
        public PlacesControllerTest()
        {
            Mapper.Initialize(cfg =>
            {
                Assembly[] assemblies = {
                Assembly.Load("Places.BLL"),
                Assembly.Load("Places.Web")

            };
                foreach (var assembly in assemblies)
                {
                    var profiles = (assembly.GetTypes())
                        .Where(t => (t.IsSubclassOf(typeof(Profile)) && t.GetConstructor(Type.EmptyTypes) != null))
                        .Select(p => (Profile)Activator.CreateInstance(p));

                    foreach (var item in profiles)
                    {
                        cfg.AddProfile(item);
                    }
                }


            });

            _placesService = new Mock<IPlaceServices>();
            _addressService = new Mock<IAddressServices>();
            _imageServices = new Mock<IImageServices>();
            _facilitiesServices = new Mock<IFacilitiesServices>();
            _userManager = new Mock<UserManager<ApplicationUser>>();
            _signInManager = new Mock<SignInManager<ApplicationUser>>();
            _placesController = new PlacesController(
                                                    _placesService.Object,
                                                    _addressService.Object,
                                                    _facilitiesServices.Object,
                                                    _imageServices.Object
                                                      /*   _logger*/)
                                                   ;
        }


        [Fact]
        public async Task IndexTest_ReturnsViewWithPlacesList()
        {
            //// Arrange
            var mockPlacesList = new PaginatedList<PlaceDTO>
            {
                new PlaceDTO {
                    Id = 32,
                    Name = "Alan Pub",

                    Address = new AddressDTO()
                        {
                            Id = 39,
                            StreetNumber = "45",
                            Street = new StreetDTO(){
                                Id = 1,
                                Name = "Stefan cel mare",

                                City = new CityDTO()
                                {
                                    Id = 2,
                                    Name = "Chisinau",
                                    Country = new CountryDTO()
                                    {
                                        Id = 1,
                                        Name = "Moldova"
                                    }
                                }
                            },

                            PostalCode = "5678"
                        },
                    AvgRating = (float?) 0,
                    Telephone = "078142739",
                    Type = new PlaceTypeDTO(){Id = 1,Name = "Pub"},
                    Email = "alan@gmail.com",
                    Website = "www.alanpub.com"
                }

            };
            int page = 3;
            int? placeType = null;
            var pageSize = 6;
            _placesService
                .Setup(repo => repo.GetPaginatedList(pageSize, placeType, null, page))
                .Returns(mockPlacesList);

            // Act
            var result = _placesController.Index(page, placeType, null);
            System.Console.WriteLine("cxcx");

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<PaginatedList<PlaceViewModel>>(viewResult.ViewData.Model);
            Assert.Equal(1, model.Count);
        }

    }
}
