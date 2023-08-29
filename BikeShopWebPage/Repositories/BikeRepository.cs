using BikeShopWebPage.Interfaces;
using BikeShopWebPage.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BikeShopWebPage.Repositories
{
    public class BikeRepository : IBikeRepository
    {
        Uri baseAddress = new Uri("https://localhost:7011/api");
        private readonly HttpClient _client;
        public BikeRepository()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }
        [HttpGet]
        public List<Bike> GetAllBikes()
        {
            List<Bike> bikeList = new List<Bike>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress+"/Bike").Result;
            if(response.IsSuccessStatusCode) 
            {
                string data = response.Content.ReadAsStringAsync().Result;  
                bikeList = JsonConvert.DeserializeObject<List<Bike>>(data);
            }
            return bikeList;
        }
    }
}
