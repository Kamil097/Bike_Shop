using BikeShopWebPage.Interfaces;
using BikeShopWebPage.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BikeShopWebPage.Controllers
{
    public class BikeController : Controller
    {
        private readonly IBikeRepository _bikeRepository;
        private readonly IUserRepository _userRepository;
        public BikeController(IBikeRepository bikeRepository, IUserRepository userRepository)
        {
            _bikeRepository = bikeRepository;
            _userRepository = userRepository;
        }
        public IActionResult BikeDetails(int id)
        {
            var bike = _bikeRepository.GetBikeById(id);
            var reviews = _bikeRepository.GetReviewsByBike(id);
            BikeReview bikeReview = new BikeReview() {bike=bike,reviews=reviews };
            if (bike == null)
            {
                return HttpNotFound(); 
            }
            return View("bikeDetails", bikeReview);
        }
        
        private IActionResult HttpNotFound()
        {
            throw new NotImplementedException();
        }

        public IActionResult Index()
        {
            var bikes = _bikeRepository.GetAllBikes();
            return View(bikes);
        }
    }
}
