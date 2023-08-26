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
	public class ContactController : Controller
	{
		private readonly IContactRepository _contactRepository;
		private readonly IUserRepository _userRepository;	
		private readonly IMapper _mapper;
        public ContactController(IContactRepository contactRepository,IUserRepository userRepository, IMapper mapper)
        {
			_userRepository = userRepository;	
			_contactRepository = contactRepository;
			_mapper = mapper;
        }

		[HttpGet]
		[ProducesResponseType(200, Type = typeof(IEnumerable<Contact>))]
		public IActionResult GetContacts() 
		{
			var contacts = _mapper.Map<List<ContactDto>>(_contactRepository.GetContacts());
			if (!ModelState.IsValid)
				return BadRequest();
			return Ok(contacts);
		}

		[HttpGet("getContact/{id}")]
		[ProducesResponseType(200, Type = typeof(Contact))]
		[ProducesResponseType(400)]
		public IActionResult GetContact(int id)
		{
			if (!_contactRepository.ContactExists(id))
				return NotFound();
			var contact = _mapper.Map<ContactDto>(_contactRepository.GetContact(id));

			if (!ModelState.IsValid)
				return BadRequest(ModelState);
			return Ok(contact);
		}
		[HttpGet("getContactByUser/{userId}")]
		[ProducesResponseType(200, Type = typeof(Contact))]
		[ProducesResponseType(400)]
		public IActionResult GetContactByUser(int userId)
		{
			if (!_userRepository.UserExists(userId))
				return NotFound();
			var contact = _mapper.Map<ContactDto>(_contactRepository.GetContactByUser(userId));

			if (!ModelState.IsValid)
				return BadRequest(ModelState);
			return Ok(contact);
		}

		[HttpPost]
		[ProducesResponseType(204)]
		[ProducesResponseType(400)]
		public IActionResult CreateContact([FromBody] ContactDto contactCreate)
		{
			if (contactCreate == null)
				return BadRequest(ModelState);

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var contactMap = _mapper.Map<Contact>(contactCreate);
			var user = _userRepository.GetUser(contactCreate.UserId);
			contactMap.User = user;
			if (!_contactRepository.CreateContact(contactMap))
			{
				ModelState.AddModelError("", "Something went wrong saving contact.");
				return StatusCode(500, ModelState);
			}
			return Ok(contactMap.Id);
		}

	}
}
