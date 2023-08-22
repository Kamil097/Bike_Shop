using AutoMapper;
using BikeShopREST.Dto;
using BikeShopREST.Interfaces;
using BikeShopREST.Models;
using BikeShopREST.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BikeShopREST.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : Controller
	{
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserController(IUserRepository userRepository,IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

		[HttpGet]
		[ProducesResponseType(200, Type = typeof(IEnumerable<User>))]
		public IActionResult GetAllUsers()
		{
			var users = _mapper.Map<List<UserDto>>(_userRepository.GetUsers());
			if (!ModelState.IsValid)
				return BadRequest(ModelState);
			return Ok(users);
		}

		[HttpGet("getUser/{userId}")]
		[ProducesResponseType(200, Type = typeof(User))]
		[ProducesResponseType(400)]
		public IActionResult GetUser(int userId)
		{
			if (!_userRepository.UserExists(userId))
				return NotFound();
			var user = _userRepository.GetUser(userId);

			if (!ModelState.IsValid)
				return BadRequest(ModelState);
			return Ok(user);
		}
		[HttpGet("getBikesByUser/{userId}")]
		[ProducesResponseType(200, Type = typeof(IEnumerable<Bike>))]
		[ProducesResponseType(400)]
		public IActionResult GetBikesByUser(int userId)
		{
			if (!_userRepository.UserExists(userId))
				return NotFound();
			var bikes = _mapper.Map<List<BikeDto>>(_userRepository.GetBikesByUser(userId));

			if (!ModelState.IsValid)
				return BadRequest(ModelState);
			return Ok(bikes);
		}
	}
}
