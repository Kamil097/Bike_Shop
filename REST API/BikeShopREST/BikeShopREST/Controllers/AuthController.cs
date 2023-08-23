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

		[HttpPost]
		[ProducesResponseType(204)]
		[ProducesResponseType(400)]
		public IActionResult CreateAuth([FromBody] AuthDto authCreate)
		{
			if (authCreate == null)
				return BadRequest(ModelState);

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var authMap = _mapper.Map<Auth>(authCreate);
			var user = _userRepository.GetUser(authCreate.UserId);
			authMap.User = user;

			if (!_authRepository.Register(authMap))
			{
				ModelState.AddModelError("", "Something went wrong saving auth.");
				return StatusCode(500, ModelState);
			}
			return Ok(authMap.Id);
		}
	}
}
