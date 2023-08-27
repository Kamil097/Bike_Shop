using BikeShopWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BikeShopWebApp.Controllers
{
    public class BikeController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7011/api");
        private readonly HttpClient _client;
        public BikeController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }
        [HttpGet]
        public IActionResult Index()
        {
            List<BikeModel> bikeList = new List<BikeModel>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Bike").Result;
            if(response.IsSuccessStatusCode) 
            {
                string data = response.Content.ReadAsStringAsync().Result;
                bikeList = JsonConvert.DeserializeObject<List<BikeModel>>(data);
            }
            return View(bikeList);
        }
    }
}
