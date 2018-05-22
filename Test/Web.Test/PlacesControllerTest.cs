using System;
using System.Collections.Generic;
using System.Reflection;
using Xunit;
using Places.BLL.Interfaces;
using Places.Domain;
using Microsoft.AspNetCore.Identity;
using Moq;
using Places.Web.Controllers;
using AutoMapper;
using Places.Web.Models.ViewModels;
using DomainUtil;
using Microsoft.AspNetCore.Mvc;
using Places.DTO;
using System.Security.Claims;
using Places.Web.Models;
using Places.Web.Models.Profiles;
using PlacesProfile = Places.BLL.Profiles.PlacesProfile;

namespace Web.Test
{
    public class PlacesControllerTest
    {
        #region PrivateMembers
      
        private readonly MapperConfiguration _config =
            new MapperConfiguration(               
                cfg =>
                {
                    cfg.AddProfile(new PlacesProfile());
                    cfg.AddProfile(new ImageProfile());
                    cfg.AddProfile(new AddressProfile());
                    cfg.AddProfile(new Places.Web.Models.Profiles.PlacesProfile());
                } );

        private Mock<IPlaceServices> _placesService;
        private IMapper _mapper;
        private Mock<IAddressServices> _addressService;
        private Mock<IImageServices> _imageServices;
        private Mock<IFacilitiesServices> _facilitiesServices;
        private Mock<UserManager<ApplicationUser>> _userManager;
        private Mock<SignInManager<ApplicationUser>> _signInManager;
        private PlacesController _placesController;
        private PaginatedList<PlaceDTO> mockPlacesList;
        private PlaceDetailsDTO placeDetails;
        private List<CountryDTO> _countryDtos;
        private List<CityDTO> _citiesDtos;
        private List<StreetDTO> _streetDtos;
        private List<PlaceTypeDTO> _placeTypeDto;
        private List<FacilitiesDTO> _facilitieDto;
        private CreatePlaceViewModel _createPlaceViewModel;
        private CreatePlaceDTO _createPlaceDTO;
        #endregion
        public PlacesControllerTest()
        {           
            #region ServicesInitialization    
            _placesService = new Mock<IPlaceServices>();
            _addressService = new Mock<IAddressServices>();
            _imageServices = new Mock<IImageServices>();
            _facilitiesServices = new Mock<IFacilitiesServices>();
            _userManager = GetMockUserManager();
            _mapper = _config.CreateMapper();
            // _signInManager = MockSignInManager<ApplicationUser>();         
            _placesController = new PlacesController(
                                                    _placesService.Object,
                                                    _addressService.Object,
                                                    _facilitiesServices.Object,
                                                    _imageServices.Object,
                                                    _mapper,
                                                    _userManager.Object
                                                    //_signInManager.Object
                                                     ) ;
            #endregion

            #region  Setup

            mockPlacesList = new PaginatedList<PlaceDTO>
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
                },
                new PlaceDTO {
                Id = 32,
                Name = "Start Pub",

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
            placeDetails = new PlaceDetailsDTO
            {
                Id = 32,
                Name = "Alan Pub",
                Descriprion = "AlanPub!",
                Images = new List<ImageDTO>(),
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
                RatingAVG = 0,
                Telephone = "078142739",
                Type = new PlaceTypeDTO() {Id = 1, Name = "Pub"},
                Email = "alan@gmail.com",
                Website = "www.alanpub.com"
            };
            _countryDtos= new List<CountryDTO>()
            {
                new CountryDTO(){Id=1,Name = "Moldova"},
                new CountryDTO(){Id=2,Name = "Romania"}

            };
            _citiesDtos = new List<CityDTO>()
            {
                new CityDTO(){Id=1,Name = "Chisinau"},
                new CityDTO(){Id=2,Name = "Orhei"}

            };
            _streetDtos = new List<StreetDTO>
            {
                new StreetDTO(){Id=1,Name = "Stefan cel Mare"},
                new StreetDTO(){Id=2,Name = "Ismail"}
            };
            _placeTypeDto = new List<PlaceTypeDTO>()
            {
                new PlaceTypeDTO(){Id = 1,Name = "Restaurant"},
                new PlaceTypeDTO(){Id = 2,Name = "Pub"},
                new PlaceTypeDTO(){Id = 3,Name = "Pizzeria"},

            };

            _facilitieDto= new List<FacilitiesDTO>()
            {
                new FacilitiesDTO(){Id = 1,Name = "Wi-Fi"},
                new FacilitiesDTO(){Id = 2,Name = "Parking"},
            };
             _createPlaceViewModel = new CreatePlaceViewModel()
            {
                Id = 1,
                Name = "Alan Pub",
                Descriprion = "AlanPub!",
                Street = 1,
                CountryId = 1,
                CityId = 1,
                PostalCode = "2034",
                Telephone = "078142739",
                Type = 1,
                Email = "alan@gmail.com",
                Website = "www.alanpub.com"
            };
           _createPlaceDTO = new CreatePlaceDTO()
            {

                Name = "Alan Pub",
                Descriprion = "AlanPub!",
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
               Telephone = "078142739",
                Type = 1,
                Email = "alan@gmail.com",
                Website = "www.alanpub.com"
            };
            #endregion
        }


        [Fact]
        public void IndexTest_ReturnsViewWithPlacesList()
        {         
            int page = 3;
                      
            _placesService
                .Setup(repo => repo.GetPaginatedList(6, null, null, page))
                .Returns(mockPlacesList);

            // Act
            var result = _placesController.Index(page, null, null);
            System.Console.WriteLine("cxcx");

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<PaginatedList<PlaceViewModel>>(viewResult.ViewData.Model);
            Assert.Equal(2, model.Count);
        }

        [Fact]
        public void DetailsTest_ReturnsViewWithPlaceDetails()
        {
            int id = 1;
            _placesService
                .Setup(repo => repo.GetPlaceDetails(id))
                .Returns(placeDetails);
            var result = _placesController.Details(1);

           var viewResult =Assert.IsType<ViewResult>(result);       
           var model = Assert.IsAssignableFrom<PlaceDetailsViewModel>(viewResult.Model);

        }

        [Fact]
        public void DetailsTest_ThrowNullReferenceException_WhenIdIsEqualOrLowerThanZero()
        {
            var resultEqualToZero = _placesController.Details(0);
            var resultLowerThanZero = _placesController.Details(-1);

            var viewResultEqualToZero = Assert.IsType<ViewResult>(resultEqualToZero);
            var model1 = Assert.IsAssignableFrom<ErrorViewModel>(viewResultEqualToZero.ViewData.Model);

            var viewResultLowerThanZero = Assert.IsType<ViewResult>(resultLowerThanZero);
            var model2 = Assert.IsAssignableFrom<ErrorViewModel>(viewResultLowerThanZero.ViewData.Model);

            Assert.Equal("The place that you are looking for doesn`t exist.", model1.ErrorMessage);
            Assert.Equal("The place that you are looking for doesn`t exist.", model2.ErrorMessage);
           }

        [Fact]
        public void DetailsTest_ReturnErrorViewWhenIdIsGreaterThanExistingValues()
        {
           
            var result = _placesController.Details(-1);
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<ErrorViewModel>(viewResult.ViewData.Model);
            Assert.Equal("The place that you are looking for doesn`t exist.", model.ErrorMessage);

        }
        [Fact]
        public void CreateTest_Get_ReturnViewResult()
        {       
            _userManager
                .Setup(repo => repo.GetUserId(It.IsAny<ClaimsPrincipal>()))
                .Returns("bc3241c8-3551-4a3e-81cd-db362478e018");

            _addressService.Setup(r => r.GetCountries())
                .Returns(_countryDtos);
            _placesService.Setup(s => s.GetTypes())
                .Returns(_placeTypeDto);
            _facilitiesServices.Setup(s => s.GetFacilities())
                .Returns(_facilitieDto);

            var result = _placesController.Create();

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void CreateTest_Post_RedirectToActionWhenModelstateIsValid()
        {
            var result = _placesController.Create(_createPlaceViewModel, null);
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);

        }
        [Fact]
        public void CreateTest_Post_ReturnErrorViewWhenModelstateIsInvalid()
        {
           _placesController.ModelState.AddModelError("error", "error");

            var result = _placesController.Create(null, null);
            var redirectToActionResult = Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void EditTest_Get_ReturnErrorViewWhenIdIsZero()
        {
            _placesService.Setup(s => s.GetById(0))
                .Throws<NullReferenceException>();
            var result =  _placesController.Edit(0);
           
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<ErrorViewModel>(viewResult.ViewData.Model);
            Assert.Equal("The place that you are looking for doesn`t exist.", model.ErrorMessage);
        }
        [Fact]
        public void EditTest_Get_ReturnErrorViewWhenIdIsLowerThanZero()
        {
            _placesService.Setup(s => s.GetById(-1))
                .Throws<NullReferenceException>();
            var result = _placesController.Edit(-1);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<ErrorViewModel>(viewResult.ViewData.Model);
            Assert.Equal("The place that you are looking for doesn`t exist.", model.ErrorMessage);
        }

        [Fact]
        public void EditTest_Get_ReturnErrorViewWhenPlaceIsNull()
        {
            CreatePlaceDTO places = null;
            _placesService.Setup(s => s.GetById(Int32.MaxValue))
                .Returns(places);
            var result = _placesController.Edit(Int32.MaxValue);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<ErrorViewModel>(viewResult.ViewData.Model);
            Assert.Equal("The place that you are looking for was not found .", model.ErrorMessage);
        }

        [Fact]
        public void EditTest_Get_ReturnViewResult()
        {
            _userManager
                .Setup(repo => repo.GetUserId(It.IsAny<ClaimsPrincipal>()))
                .Returns("bc3241c8-3551-4a3e-81cd-db362478e018");
            _addressService.Setup(r => r.GetCountries())
                .Returns(_countryDtos);
            _addressService.Setup(r => r.GetCities(1))
                .Returns(_citiesDtos);
            _addressService.Setup(r => r.GetStreets(1,1))
                .Returns(_streetDtos);
            _placesService.Setup(s => s.GetTypes())
                .Returns(_placeTypeDto);         
            _placesService.Setup(s => s.GetById(1))
                .Returns(_createPlaceDTO);

            var result = _placesController.Edit(1);
                   
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<CreatePlaceViewModel>(viewResult.ViewData.Model);
        }
        [Fact]
        public void EditTest_Post_ReturnErrorViewResultWhenIdDiffrentFromModelId()
        {
            var result = _placesController.Edit(2,_createPlaceViewModel,null);
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<ErrorViewModel>(viewResult.ViewData.Model);
        }
        [Fact]
        public void EditTest_Post_ReturnRedirectToActionResultWhenModelStateIsValid()
        {
            var result = _placesController.Edit(1, _createPlaceViewModel, null);
            var viewResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Details", viewResult.ActionName);
            Assert.Null( viewResult.ControllerName);

        }
        [Fact]
        public void EditTest_Post_ReturnErrorViewWhenModelStateIsInvalid()
        {
            _placesController.ModelState.AddModelError("error", "error");

            var result = _placesController.Edit(1, _createPlaceViewModel, null);
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<ErrorViewModel>(viewResult.ViewData.Model);
            Assert.Equal("The information in the fields is not valid . Please try again",model.ErrorMessage);

        }

        [Fact]
        public void DeleteTest_ReturnErrorViewWhenIdIsEqualToZero()
        {
         
            _placesService.Setup(s => s.GetById(0))
                .Throws<NullReferenceException>();
            var result = _placesController.Delete(0);
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<ErrorViewModel>(viewResult.ViewData.Model);
            Assert.Equal("The place that you are looking for was not found ..", model.ErrorMessage);

        }

        [Fact]
        public void DeleteTest_ReturnErrorViewWhenIdIsLowerThanZero()
        {
            _placesService.Setup(s => s.GetById(-1))
                .Throws<NullReferenceException>();
            var result = _placesController.Delete(-1);
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<ErrorViewModel>(viewResult.ViewData.Model);
            Assert.Equal("The place that you are looking for was not found ..", model.ErrorMessage);

        }

        [Fact]
        public void DeleteTest_ReturnErrorViewWhenIdIsGreaterThanExistingValues()
        {
            _placesService.Setup(s => s.GetById(Int32.MaxValue))
                .Throws<NullReferenceException>();
            var result = _placesController.Delete(Int32.MaxValue);
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<ErrorViewModel>(viewResult.ViewData.Model);
            Assert.Equal("The place that you are looking for was not found ..", model.ErrorMessage);

        }


        [Fact]
        public void DeleteTest_ReturnRedirectToAction()
        {
            _placesService.Setup(s => s.GetById(18))
                .Returns(_createPlaceDTO);
            _placesService.Setup(s => s.DeletePlace(18))
                .Verifiable();
            var result = _placesController.Delete(18);
          var viewResult =Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", viewResult.ActionName);
            Assert.Null(viewResult.ControllerName);
        }

     

        private Mock<UserManager<ApplicationUser>> GetMockUserManager()
        {
            var userStoreMock = new Mock<IUserStore<ApplicationUser>>();
            return new Mock<UserManager<ApplicationUser>>(
                userStoreMock.Object, null, null, null, null, null, null, null, null);
        }
    }
}
