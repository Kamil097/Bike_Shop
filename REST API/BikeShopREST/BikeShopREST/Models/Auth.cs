namespace BikeShopREST.Models
{
	public class Auth
	{
        public int Id { get; set; }
        public string Username { get; set; }	
		public string PasswordHash { get; set; }
        public User User { get; set; }

    }
}
