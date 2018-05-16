using AutoMapper;
using Places.BLL.Interfaces;
using Places.DAL.Interfaces;
using Places.Domain;
using Places.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Places.BLL.Services
{
    public class ReviewsServices : IReviewsServices
    {
        private IRepository<Review> _reviewRepository;

        public ReviewsServices(IRepository<Review> reviewRepository )
        {
            _reviewRepository = reviewRepository;
        }

        public void AddReview(ReviewDTO review)
        {

            var newReview = new Review
            {
                Date = DateTime.UtcNow,
                PlaceId = review.PlaceId,
                Description = review.Description,
                Rating = review.Rating,
                ApplicationUserId = review.ApplicationUserId
            };


            _reviewRepository.Add(newReview);
            _reviewRepository.SaveChanges();
        }
        
    }
}
