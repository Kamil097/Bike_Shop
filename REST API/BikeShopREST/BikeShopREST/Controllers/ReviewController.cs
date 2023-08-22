using AutoMapper;
using BikeShopREST.Interfaces;
using BikeShopREST.Models;
using Microsoft.AspNetCore.Mvc;

namespace BikeShopREST.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ReviewController : Controller
	{
        private readonly IReviewRepository _reviewRepository;
		private readonly IBikeRepository _bikeRepository;
        private readonly IMapper _mapper;
        public ReviewController(IBikeRepository bikeRepository,IReviewRepository reviewRepository,IMapper mapper)
        {
            _reviewRepository = reviewRepository;
			_bikeRepository = bikeRepository;
            _mapper = mapper;
        }
        [HttpGet("getReview/{reviewId}")]
        [ProducesResponseType(200,Type=typeof(Review))]
        [ProducesResponseType(400)]
        public IActionResult GetReview(int reviewId)
        {
            if (!_reviewRepository.ReviewExists(reviewId))
                return NotFound();
            var review = _reviewRepository.GetReview(reviewId);
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(review);
        }
		[HttpGet]
		[ProducesResponseType(200, Type = typeof(IEnumerable<Review>))]
		public IActionResult GetReviews()
		{
			var reviews = _reviewRepository.GetReviews();
			if (!ModelState.IsValid)
				return BadRequest(ModelState);
			return Ok(reviews);
		}
		[HttpGet("getReviewsByBike/{bikeId}")]
		[ProducesResponseType(200, Type = typeof(IEnumerable<Review>))]
		[ProducesResponseType(400)]
		public IActionResult GetReviewsByBike(int bikeId)
		{
			if (!_bikeRepository.BikeExists(bikeId))
				return NotFound();
			var reviews = _reviewRepository.GetReviewsByBike(bikeId);
			if (!ModelState.IsValid)
				return BadRequest(ModelState);
			return Ok(reviews);
		}
	}
}
