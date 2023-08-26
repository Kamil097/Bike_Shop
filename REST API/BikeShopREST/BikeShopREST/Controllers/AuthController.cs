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
	public class AuthController : Controller
	{
		IUserRepository _userRepository;
        IAuthRepository _authRepository;
		IMapper _mapper;

        public AuthController(IAuthRepository authRepository,IMapper mapper,IUserRepository userRepository)
        {
			_userRepository = userRepository;
            _authRepository = authRepository;
			_mapper = mapper;
        }

		[HttpPost("update/{userId}")]
		[ProducesResponseType(204)]
		[ProducesResponseType(400)]
		public IActionResult CreateAuth([FromBody] AuthDto authCreate, int userId)
		{
			if (authCreate == null)
				return BadRequest(ModelState);

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var authMap = _mapper.Map<Auth>(authCreate);
			var user = _userRepository.GetUser(userId);
			authMap.User = user;

			if (!_authRepository.Register(authMap))
			{
				ModelState.AddModelError("", "Something went wrong saving auth.");
				return StatusCode(500, ModelState);
			}
			return Ok(authMap.Id);
		}
		[HttpPut("update/{authId}")]
		[ProducesResponseType(400)]
		[ProducesResponseType(204)]
		[ProducesResponseType(404)]
		public IActionResult UpdateAuth([FromBody] AuthDto authUpdate, int authId)
		{
			if (authUpdate == null)
				return BadRequest(ModelState);
			if (!_authRepository.AuthExists(authId))
				return NotFound();
			if (authUpdate.Id != authId)
				return BadRequest(ModelState);
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var authMap = _mapper.Map<Auth>(authUpdate);
			authMap.UserId = _authRepository.GetUserByAuth(authId).Id;
			if (!_authRepository.UpdateUserData(authMap))
			{
				ModelState.AddModelError("", "Something went wrong updating user data.");
				return StatusCode(500, ModelState);
			}
			return NoContent();
		}
	}
}
