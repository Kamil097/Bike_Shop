namespace BikeShopREST.Models
{
	public class Bike
	{
        public int Id { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public float Price { get; set; }
        public float Rating { get; set; }
        public ICollection<BikeCategory> BikeCategories { get; set; }
        public ICollection<BikeUser> BikeUsers { get; set; }
        public ICollection<Review> Reviews { get; set; }
    }
}
