using BikeShopWebPage.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BikeShopWebPage.Controllers
{
    public class UserController : Controller
    {
        public readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public IActionResult Index()
        {
            var user = _userRepository.GetUser(1);
            return View(user);
        }

        public IActionResult Basket()
        {
            var bikes = _userRepository.GetBikesByUser(1);
            return View(bikes);
        }
       
    }
}
