using BikeShopREST.Models;

namespace BikeShopREST.Interfaces
{
	public interface IUserRepository
	{
		ICollection<User> GetUsers();
		User GetUser(int id);
		ICollection<Bike> GetBikesByUser(int userId);
		bool UserExists(int id);
		bool Save();
	}
}
