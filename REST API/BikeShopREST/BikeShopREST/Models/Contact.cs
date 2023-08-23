namespace BikeShopREST.Models
{
	public class Contact
	{
        public int Id { get; set; }
        public int PhoneNo { get; set; }
        public string Email { get; set; }
		public ICollection<User> User { get; set; }
    }
}
