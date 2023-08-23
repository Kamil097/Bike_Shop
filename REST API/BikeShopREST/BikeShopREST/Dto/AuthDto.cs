namespace BikeShopREST.Dto
{
	public class AuthDto
	{
		public int Id { get; set; }
		public string Username { get; set; }
		public string PasswordHash { get; set; }
		public int UserId { get; set; }
	}
}
