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
		[HttpPost]
		[ProducesResponseType(204)]
		[ProducesResponseType(400)]
		public IActionResult CreateBike([FromBody] BikeDto bikeCreate)
		{
			if (bikeCreate == null)
				return BadRequest(ModelState);

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var bikeMap = _mapper.Map<Bike>(bikeCreate);
			if (!_bikeRepository.CreateBike(bikeMap))
			{
				ModelState.AddModelError("", "Something went wrong saving bike.");
				return StatusCode(500, ModelState);
			}
			return Ok(bikeMap.Id);
		}
		[HttpPut("update/{bikeId}")]
		[ProducesResponseType(400)]
		[ProducesResponseType(204)]
		[ProducesResponseType(404)]
		public IActionResult UpdateBike([FromBody] BikeDto bikeUpdate, int bikeId)
		{
			if (bikeUpdate == null)
				return BadRequest(ModelState);
			if (!_bikeRepository.BikeExists(bikeId))
				return NotFound();
			if (bikeUpdate.Id != bikeId)
				return BadRequest(ModelState);
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var bikeMap = _mapper.Map<Bike>(bikeUpdate);
			if (!_bikeRepository.UpdateBike(bikeMap))
			{
				ModelState.AddModelError("", "Something went wrong updating bike data.");
				return StatusCode(500, ModelState);
			}
			return NoContent();
		}
	}
}
