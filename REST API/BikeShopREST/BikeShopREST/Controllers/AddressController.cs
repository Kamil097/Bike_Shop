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
	public class AddressController:Controller
	{
        private readonly IAddressRepository _addressRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public AddressController(IMapper mapper, IAddressRepository addressRepository,IUserRepository userRepository)
        {
            _addressRepository = addressRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Address>))]
        public IActionResult GetAllAddresses()
        {
            var addresses = _mapper.Map<List<AddressDto>>(_addressRepository.GetAddresses());
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(addresses);
        }
        [HttpGet("getAddress/{addresId}")]
        [ProducesResponseType(200,Type=typeof(Address))]
        [ProducesResponseType(400)]
        public IActionResult GetAddress(int addresId)
        {
            if (!_addressRepository.AddressExists(addresId))
                return NotFound();
            var address = _mapper.Map<AddressDto>(_addressRepository.GetAddress(addresId));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(address);
        }
        [HttpGet("getAddressByUser/{userId}")]
        [ProducesResponseType(200, Type = typeof(Address))]
        [ProducesResponseType(400)]
        public IActionResult GetAddressByUser(int userId)
        {
            if (!_userRepository.UserExists(userId))
                return NotFound();
            var address =_mapper.Map<AddressDto>(_addressRepository.GetAddressByUser(userId));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(address);
        }
		[HttpGet("getUserByAddress/{addressId}")]
		[ProducesResponseType(200, Type = typeof(User))]
		[ProducesResponseType(400)]
		public IActionResult GetUserByAddress(int addressId)
		{
			if (!_addressRepository.AddressExists(addressId))
				return NotFound();

			var user = _mapper.Map<List<UserDto>>(_addressRepository.GetUserByAddress(addressId));
			if (!ModelState.IsValid)
				return BadRequest(ModelState);
			return Ok(user);
		}
		[HttpPost]
		[ProducesResponseType(204)]
		[ProducesResponseType(400)]
		public IActionResult CreateAddress([FromBody] AddressDto addressCreate)
		{
			if (addressCreate == null)
				return BadRequest(ModelState);
			
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var addressMap = _mapper.Map<Address>(addressCreate);
			if (!_addressRepository.CreateAddress(addressMap))
			{
				ModelState.AddModelError("", "Something went wrong saving address.");
				return StatusCode(500, ModelState);
			}
			return Ok(addressMap.Id);
		}
	}
}
