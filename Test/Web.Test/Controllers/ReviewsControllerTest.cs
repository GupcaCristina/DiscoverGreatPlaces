using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Places.BLL.Interfaces;
using Places.DTO;
using Places.Web.Controllers;
using Places.Web.Models.Profiles;
using Places.Web.Models.ViewModels;
using Xunit;

namespace Web.Controllers.Test
{
    public class ReviewsControllerTest
    {
        private readonly Mock<IReviewsServices> _reviewsServices;
        private ReviewsController _reviewsController;
        private readonly MapperConfiguration _config =
            new MapperConfiguration(
                cfg =>
                {
                    cfg.AddProfile(new PlacesProfile());
                    cfg.AddProfile(new ImageProfile());
                    cfg.AddProfile(new AddressProfile());
                    cfg.AddProfile(new Places.Web.Models.Profiles.PlacesProfile());
                });
        private IMapper _mapper;

        public ReviewsControllerTest()
        {
            _reviewsServices= new Mock<IReviewsServices>();
            _mapper = _config.CreateMapper();

            _reviewsController = new ReviewsController(_reviewsServices.Object,_mapper);
        }

        [Fact]
        public void CreateTest_Post_ResturnRedirectToActionResultWhenModelStateIsValid()
        {
            _reviewsServices.Setup(s=>s.AddReview(new ReviewDTO()
                {
                    ApplicationUserId = "324hj3h4j3h",
                    Description = "Good",
                    PlaceId = 18,
                    Rating = 10
                }))
                .Verifiable();


            var result = _reviewsController.Create(new ReviewViewModel()
            {
                ApplicationUserId = "324hj3h4j3h",
                Description = "Good",
                PlaceId = 18,
                Rating = 10
            });
            Assert.IsType<RedirectToActionResult>(result);
        }


        [Fact]
        public void CreateTest_Post_ResturnRedirectToActionResultWhenModelStateIsInvalid()
        {
            _reviewsServices.Setup(s => s.AddReview(new ReviewDTO()
                {
                    ApplicationUserId = "324hj3h4j3h",
                    Description = "Good",
                    PlaceId = 18,
                    Rating = 10
                }))
                .Verifiable();

            var result = _reviewsController.Create(new ReviewViewModel()
            {
                ApplicationUserId = "324hj3h4j3h",
                Rating = 10
            });

            Assert.IsType<RedirectToActionResult>(result);
        }
    }
}
