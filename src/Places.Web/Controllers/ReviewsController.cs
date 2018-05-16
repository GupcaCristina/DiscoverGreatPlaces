using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Places.BLL.Interfaces;
using Places.DTO;
using Places.Web.Models.ViewModels;

namespace Places.Web.Controllers
{
    public class ReviewsController : Controller
    {

        private readonly IReviewsServices _reviewsServices;

        public ReviewsController(IReviewsServices reviewsServices)

        {
            _reviewsServices = reviewsServices;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ReviewViewModel review)
        {
            if (ModelState.IsValid)
            {
                var newReview = Mapper.Map<ReviewDTO>(review);
                _reviewsServices.AddReview(newReview);
            }
            return RedirectToAction("Details", "Places", new { id = review.PlaceId });


        }


    }
}
