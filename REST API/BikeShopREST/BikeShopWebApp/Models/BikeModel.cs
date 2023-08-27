using Microsoft.AspNetCore.Mvc.ViewEngines;

namespace BikeShopWebApp.Models
{
    public class BikeModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public float Price { get; set; }
        public float Rating { get; set; }
    }
}
