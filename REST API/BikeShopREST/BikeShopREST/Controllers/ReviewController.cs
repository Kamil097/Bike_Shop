using AutoMapper;
using BikeShopREST.Dto;
using BikeShopREST.Interfaces;
using BikeShopREST.Models;
using BikeShopREST.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BikeShopREST.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ReviewController : Controller
	{
        private readonly IReviewRepository _reviewRepository;
		private readonly IBikeRepository _bikeRepository;
		private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public ReviewController(IBikeRepository bikeRepository,IReviewRepository reviewRepository,IUserRepository userRepository,IMapper mapper)
        {
            _reviewRepository = reviewRepository;
			_bikeRepository = bikeRepository;
			_userRepository = userRepository;
            _mapper = mapper;
        }
        [HttpGet("getReview/{reviewId}")]
        [ProducesResponseType(200,Type=typeof(Review))]
        [ProducesResponseType(400)]
        public IActionResult GetReview(int reviewId)
        {
            if (!_reviewRepository.ReviewExists(reviewId))
                return NotFound();
            var review = _mapper.Map<ReviewDto>(_reviewRepository.GetReview(reviewId));
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(review);
        }
		[HttpGet]
		[ProducesResponseType(200, Type = typeof(IEnumerable<Review>))]
		public IActionResult GetReviews()
		{
			var reviews = _mapper.Map<List<ReviewDto>>(_reviewRepository.GetReviews());
			if (!ModelState.IsValid)
				return BadRequest(ModelState);
			return Ok(reviews);
		}
		[HttpGet("getReviewsByUser/{userId}")]
		[ProducesResponseType(200, Type = typeof(IEnumerable<Review>))]
		[ProducesResponseType(400)]
		public IActionResult GetReviewsByuser(int userId)
		{
			if (!_bikeRepository.BikeExists(userId))
				return NotFound();
			var reviews = _mapper.Map<List<ReviewDto>>(_reviewRepository.GetReviewsByUser(userId));
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
			var reviews = _mapper.Map<List<ReviewDto>>(_reviewRepository.GetReviewsByBike(bikeId));
			if (!ModelState.IsValid)
				return BadRequest(ModelState);
			return Ok(reviews);
		}
		[HttpPost]
		[ProducesResponseType(204)]
		[ProducesResponseType(400)]
		public IActionResult CreateReview([FromBody] ReviewDto reviewCreate, [FromQuery]int bikeId, [FromQuery] int userId)
		{
			if (reviewCreate == null)
				return BadRequest(ModelState);

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var reviewMap = _mapper.Map<Review>(reviewCreate);
			var user = _userRepository.GetUser(userId);
			var bike = _bikeRepository.GetBike(bikeId);
			reviewMap.Bike = bike;
			reviewMap.User = user;
			if (!_reviewRepository.CreateReview(reviewMap))
			{
				ModelState.AddModelError("", "Something went wrong saving review.");
				return StatusCode(500, ModelState);
			}
			return Ok(reviewMap.Id);
		}
		[HttpPut("update/{reviewId}")]
		[ProducesResponseType(400)]
		[ProducesResponseType(204)]
		[ProducesResponseType(404)]
		public IActionResult UpdateReview([FromBody] ReviewDto reviewUpdate, int reviewId)
		{
			if (reviewUpdate == null)
				return BadRequest(ModelState);
			if (!_reviewRepository.ReviewExists(reviewId))
				return NotFound();
			if (reviewUpdate.Id != reviewId)
				return BadRequest(ModelState);
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var reviewMap = _mapper.Map<Review>(reviewUpdate);
			if (!_reviewRepository.UpdateReview(reviewMap))
			{
				ModelState.AddModelError("", "Something went wrong updating review data.");
				return StatusCode(500, ModelState);
			}
			return NoContent();
		}
	}
}
