using BikeShopWebPage.Interfaces;
using BikeShopWebPage.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BikeShopWebPage.Repositories
{
    public class UserRepository : IUserRepository
    {
        Uri baseAddress = new Uri("https://localhost:7011/api");
        private readonly HttpClient _client;
        public UserRepository()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }
        [HttpGet]
        public User GetUser(int id)
        {
            User user = new User();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + $"/User/getUser/{id}").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                user = JsonConvert.DeserializeObject<User>(data);
            }
            return user;
        }

        public Address GetUserAddress(int userId)
        {
            Address address = new Address();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + $"/Address/getAddressByUser/{userId}").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                address = JsonConvert.DeserializeObject<Address>(data);
            }
            return address;
        }

        public Contact GetUserContact(int userId)
        {
            Contact contact = new Contact();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + $"/Contact/getContactByUser/{userId}").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                contact = JsonConvert.DeserializeObject<Contact>(data);
            }
            return contact;
        }

        public List<Review> GetUserReviews(int userId)
        {
            List<Review> reviews = new List<Review>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + $"/Review/getReviewsByuser/{userId}").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                reviews = JsonConvert.DeserializeObject<List<Review>>(data);
            }
            return reviews;
        }

        public List<Bike> GetBikesByUser(int userId)
        {
            List<Bike> bikes = new List<Bike>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + $"/BikeUser/getBikesByUser/{userId}").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                bikes = JsonConvert.DeserializeObject<List<Bike>>(data);
            }
            return bikes;
        }
    }
}
