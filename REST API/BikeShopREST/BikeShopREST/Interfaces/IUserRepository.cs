using BikeShopREST.Models;

namespace BikeShopREST.Interfaces
{
	public interface IUserRepository
	{
		ICollection<User> GetUsers();
		User GetUser(int id);
		ICollection<Bike> GetBikesByUser(int userId);
		bool AssignBikeToUser(int userId, int bikeId);
		bool DeleteBikeFromUser(BikeUser bikeuser);
		bool BikeUserExists(int bikeId, int userId);
		BikeUser GetBikeUser(int bikeId, int userId);
		bool CreateUser(User user);
		bool UpdateUser(User User);
		bool DeleteUser(User user);
		bool UserExists(int id);
		bool Save();
	}
}
