using BikeShopREST.Models;

namespace BikeShopREST.Interfaces
{
	public interface IAuthRepository
	{
		bool Register(Auth auth);
		bool UpdateUserData(Auth auth);
		Auth GetAuthByUser(int userId);
		User GetUserByAuth(int authId);
		bool AuthExists(int authId);
		bool Save();
	}
}
