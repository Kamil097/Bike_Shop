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
    }
}
