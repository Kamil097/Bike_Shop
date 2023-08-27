using AutoMapper;
using BikeShopREST.Dto;
using BikeShopREST.Interfaces;
using BikeShopREST.Models;
using Microsoft.AspNetCore.Mvc;

namespace BikeShopREST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BikeUserController : Controller
    {
        private readonly IbikeUserRepository _bikeUserRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public BikeUserController(IbikeUserRepository bikeUserRepository, IMapper mapper, IUserRepository userRepository)
        {
            _bikeUserRepository = bikeUserRepository;
            _mapper = mapper;
            _userRepository = userRepository;

        }
        [HttpGet("getBikesByUser/{userId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Bike>))]
        [ProducesResponseType(400)]
        public IActionResult GetBikesByUser(int userId)
        {
            if (!_userRepository.UserExists(userId))
                return NotFound();
            var bikes = _mapper.Map<List<BikeDto>>(_bikeUserRepository.GetBikesByUser(userId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(bikes);
        }
        [HttpPost("AssignBikeToUser")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult AssignBikeToUser([FromQuery] int bikeId, [FromQuery] int userId)
        {
            var bike = _bikeUserRepository.GetBikesByUser(userId)
                .Where(b => b.Id == bikeId)
                .FirstOrDefault();

            if (bike != null)
            {
                ModelState.AddModelError("", $"User already has this bike.");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            if (!_bikeUserRepository.AssignBikeToUser(userId, bikeId))
            {
                ModelState.AddModelError("", "Something went wrong while saving.");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully added.");
        }
        [HttpDelete("delete/{bikeId}/{userId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteBikeFromUser(int bikeId, int userId)
        {
            if (!_bikeUserRepository.BikeUserExists(bikeId, userId))
                return NotFound();
            var bikeUserDelete = _bikeUserRepository.GetBikeUser(bikeId, userId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (_bikeUserRepository.DeleteBikeFromUser(bikeUserDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting bike from user.");
            }
            return NoContent();
        }
    }
}
