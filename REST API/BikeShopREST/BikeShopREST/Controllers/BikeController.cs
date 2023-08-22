using AutoMapper;
using BikeShopREST.Dto;
using BikeShopREST.Interfaces;
using BikeShopREST.Models;
using Microsoft.AspNetCore.Mvc;

namespace BikeShopREST.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BikeController:Controller
	{
        private readonly IBikeRepository _bikeRepository;
        private readonly IMapper _mapper;
        public BikeController(IMapper mapper, IBikeRepository bikeRepository)
        {
            _bikeRepository = bikeRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Bike>))]
        public IActionResult GetBikes()
        {
            var bikes = _mapper.Map<List<BikeDto>>(_bikeRepository.GetBikes());
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(bikes);
        }
        [HttpGet("getBike/{bikeId}")]
        [ProducesResponseType(200, Type = typeof(Bike))]
        [ProducesResponseType(400)]
        public IActionResult GetBike(int bikeId)
        {
            if (!_bikeRepository.BikeExists(bikeId))
                return NotFound();
            var bike = _mapper.Map<BikeDto>(_bikeRepository.GetBike(bikeId));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(bike);
        }
    }
}
