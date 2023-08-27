using AutoMapper;
using BikeShopREST.Dto;
using BikeShopREST.Interfaces;
using BikeShopREST.Models;
using BikeShopREST.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace BikeShopREST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BikeCategoryController : Controller
    {
        public readonly IBikeCategoryRepository _bikeCategoryRepository;
        public readonly IBikeRepository _bikeRepository;
        public readonly ICategoryRepository _categoryRepository;
        public readonly IMapper _mapper;
        public BikeCategoryController(IMapper mapper,IBikeCategoryRepository bikeCategoryRepository, ICategoryRepository categoryRepository,IBikeRepository bikeRepository)
        {
            _bikeCategoryRepository = bikeCategoryRepository;
            _bikeRepository = bikeRepository;   
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        [HttpGet("getBikeByCategory/{categoryId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Bike>))]
        [ProducesResponseType(400)]
        public IActionResult GetBikesByCategory(int categoryId) 
        {
            if (!_categoryRepository.CategoryExists(categoryId))
                return NotFound();
            var bikes = _mapper.Map<List<BikeDto>>(_bikeCategoryRepository.GetBikesByCategory(categoryId));
            if(bikes.Count==0)
                return NotFound();  
            if(!ModelState.IsValid)
                return BadRequest(ModelState);  
            return Ok(bikes);
        }
		[HttpGet("getCategoryByBike/{bikeId}")]
		[ProducesResponseType(200, Type = typeof(Category))]
		[ProducesResponseType(400)]
		public IActionResult GetCategoryByBike(int bikeId)
		{
			if (!_bikeRepository.BikeExists(bikeId))
				return NotFound();
			var category = _mapper.Map<CategoryDto>(_bikeCategoryRepository.GetCategoryByBike(bikeId));

            if(category==null)
                return NotFound();

			if (!ModelState.IsValid)
				return BadRequest(ModelState);
			return Ok(category);
		}
		[HttpPost]
		[ProducesResponseType(204)]
		[ProducesResponseType(400)]
		public IActionResult CreateBikeCategory([FromQuery] int bikeId, [FromQuery] int categoryId)
		{
			var category = _bikeCategoryRepository.GetBikesByCategory(categoryId)
				.Where(b => b.Id == bikeId)
				.FirstOrDefault();

			if (category != null)
			{
				ModelState.AddModelError("", $"Bike already has a category");
				return StatusCode(422, ModelState);
			}
			if (!ModelState.IsValid)
				return BadRequest(ModelState);


			if (!_bikeCategoryRepository.AssignBikeToCategory(categoryId, bikeId))
			{
				ModelState.AddModelError("", "Something went wrong while saving.");
				return StatusCode(500, ModelState);
			}
			return Ok("Successfully added.");

		}
        [HttpDelete("delete/{bikeId}/{categoryId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteBikeCategory(int bikeId,int categoryId)
        {
            if (!_bikeCategoryRepository.BikeCategoryExists(bikeId,categoryId))
                return NotFound();
            var bikeCategoryDelete = _bikeCategoryRepository.GetBikeCategory(bikeId,categoryId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_bikeCategoryRepository.DeleteBikeCategory(bikeCategoryDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting bikeCategory.");
            }
            return NoContent();
        }
    }
}
