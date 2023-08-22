using AutoMapper;
using BikeShopREST.Dto;
using BikeShopREST.Interfaces;
using BikeShopREST.Models;
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
			if (!ModelState.IsValid)
				return BadRequest(ModelState);
			return Ok(category);
		}
	}
}
