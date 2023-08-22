namespace BikeShopREST.Models
{
	public class User
	{
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Contact Contact { get; set; }
        public Address Address { get; set; }
        public Auth Auth { get; set; }
        public ICollection<BikeUser> BikeUsers { get; set; }
        public ICollection<Review> Reviews { get; set; }
    }
}
