namespace BikeShopREST.Models
{
	public class BikeUser
	{
        public int BikeId { get; set; }
        public int UserId { get; set; }
        public Bike Bike { get; set; }
        public User User { get; set; }
    }
}
