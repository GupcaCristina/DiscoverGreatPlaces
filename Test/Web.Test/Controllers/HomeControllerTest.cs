using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using AutoMapper;
using Places.BLL.Profiles;
using Places.Web.Models.Profiles;
using Places.BLL.Interfaces;
using Places.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Places.DTO;
using Places.Test.Web.Test.Controllers;
using Places.Web.Controllers;

namespace Web.Controllers.Test
{
    public class HomeControllerTest
    {
        private readonly MapperConfiguration _config =
            new MapperConfiguration(
                cfg =>
                {
                   cfg.AddProfile(new Places.BLL.Profiles.PlacesProfile());
                   cfg.AddProfile(new ImageProfile());
                    cfg.AddProfile(new AddressProfile());
                    cfg.AddProfile(new Places.Web.Models.Profiles.PlacesProfile());
                });
        private IMapper _mapper;
        private Mock<UserManager<ApplicationUser>> _userManager;
        private Mock<IPlaceServices> _placesService;
        private Mock<IImageServices> _imageServices;
        private HomeController _homeController;

        private List<PlaceDTO>  placesList;

        public HomeControllerTest()
        {
            _mapper = _config.CreateMapper();
            _userManager = UserManagerProvider.GetMockUserManager();
            _placesService = new Mock<IPlaceServices>();
            _imageServices = new Mock<IImageServices>();
            _homeController = new HomeController(_userManager.Object,
                _placesService.Object,
                _imageServices.Object
                , _mapper);

            placesList = new List<PlaceDTO>
            {
                new PlaceDTO
                {
                    Id = 32,
                    Name = "Alan Pub",

                    Address = new AddressDTO()
                    {
                        Id = 39,
                        StreetNumber = "45",
                        Street = new StreetDTO()
                        {
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
                    Type = new PlaceTypeDTO() {Id = 1, Name = "Pub"},
                    Email = "alan@gmail.com",
                    Website = "www.alanpub.com"
                },
                new PlaceDTO
                {
                    Id = 32,
                    Name = "Start Pub",

                    Address = new AddressDTO()
                    {
                        Id = 39,
                        StreetNumber = "45",
                        Street = new StreetDTO()
                        {
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
                    Type = new PlaceTypeDTO() {Id = 1, Name = "Pub"},
                    Email = "alan@gmail.com",
                    Website = "www.alanpub.com"
                }
            };
        }

        [Fact]
        public void IndexTest_ReturnsIndexView()
        {
            _placesService.Setup(s => s.GetTopPlaces(3))
                .Returns(placesList);
            _placesService.Setup(s => s.GetTypes())
                .Returns(new List<PlaceTypeDTO>()
                {
                    new PlaceTypeDTO() {Id = 1, Name = "Pub"}
                });

            var result = _homeController.Index();
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void ErrorTest_ReturnsViewResult()
        {
            var result = _homeController.Error();
            Assert.IsType<ViewResult>(result);

        }
    }
}
