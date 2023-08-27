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
		private readonly IContactRepository _contactRepository;
		private readonly IAddressRepository _addressRepository;
		private readonly IMapper _mapper;
		public UserController(IContactRepository contactRepository, IAddressRepository addressRepository, IUserRepository userRepository, IMapper mapper)
		{
			_userRepository = userRepository;
			_contactRepository = contactRepository;
			_addressRepository = addressRepository;
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
			var user = _mapper.Map<UserDto>(_userRepository.GetUser(userId));

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

		[HttpPost("CreateUser")]
		[ProducesResponseType(204)]
		[ProducesResponseType(400)]
		public IActionResult CreateUser([FromBody] UserDto userCreate, [FromQuery] int addressId)
		{
			if (userCreate == null)
				return BadRequest(ModelState);

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var userMap = _mapper.Map<User>(userCreate);
			var address = _addressRepository.GetAddress(addressId);
			userMap.Address = address;

			if (!_userRepository.CreateUser(userMap))
			{
				ModelState.AddModelError("", "Something went wrong saving user.");
				return StatusCode(500, ModelState);
			}
			return Ok(userMap.Id);
		}



		[HttpPost("AssignBikeToUser")]
		[ProducesResponseType(204)]
		[ProducesResponseType(400)]
		public IActionResult AssignBikeToUser([FromQuery] int bikeId, [FromQuery] int userId)
		{
			var bike = _userRepository.GetBikesByUser(userId)
				.Where(b => b.Id == bikeId)
				.FirstOrDefault();

			if (bike != null)
			{
				ModelState.AddModelError("", $"User already has this bike.");
				return StatusCode(422, ModelState);
			}
			if (!ModelState.IsValid)
				return BadRequest(ModelState);


			if (!_userRepository.AssignBikeToUser(userId, bikeId))
			{
				ModelState.AddModelError("", "Something went wrong while saving.");
				return StatusCode(500, ModelState);
			}
			return Ok("Successfully added.");

		}
		[HttpPut("update/{userId}")]
		[ProducesResponseType(400)]
		[ProducesResponseType(204)]
		[ProducesResponseType(404)]
		public IActionResult UpdateUser([FromBody] UserDto userUpdate, int userId)
		{
			if (userUpdate == null)
				return BadRequest(ModelState);
			if (!_contactRepository.ContactExists(userId))
				return NotFound();
			if (userUpdate.Id != userId)
				return BadRequest(ModelState);
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var userMap = _mapper.Map<User>(userUpdate);
			if (!_userRepository.UpdateUser(userMap))
			{
				ModelState.AddModelError("", "Something went wrong updating user data.");
				return StatusCode(500, ModelState);
			}
			return NoContent();
		}
        [HttpDelete("delete/{userId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteUser(int userId)
        {
            if (!_userRepository.UserExists(userId))
                return NotFound();
            var userDelete= _userRepository.GetUser(userId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!_userRepository.DeleteUser(userDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting user.");
            }
            return NoContent();
        }
        [HttpDelete("delete/{bikeId}/{userId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteBikeFromUser(int bikeId, int userId)
        {
			if (!_userRepository.BikeUserExists(bikeId, userId))
                return NotFound();
            var bikeUserDelete = _userRepository.GetBikeUser(bikeId, userId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_userRepository.DeleteBikeFromUser(bikeUserDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting bike from user.");
            }
            return NoContent();
        }
    }
}
