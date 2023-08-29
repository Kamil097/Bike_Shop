using BikeShopWebPage.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BikeShopWebPage.Controllers
{
    public class BikeController : Controller
    {
        private readonly IBikeRepository _bikeRepository;
        public BikeController(IBikeRepository bikeRepository)
        {
            _bikeRepository = bikeRepository;
        }

        public IActionResult Index()
        {
            var bikes = _bikeRepository.GetAllBikes();
            return View(bikes);
        }
    }
}
