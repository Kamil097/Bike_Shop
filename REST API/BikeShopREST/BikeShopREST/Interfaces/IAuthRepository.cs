using BikeShopREST.Models;

namespace BikeShopREST.Interfaces
{
	public interface IAuthRepository
	{
		bool Register(Auth Auth);
		bool UpdateUserData(Auth Auth);
		Auth GetAuthByUser(int userId);
		User GetUserByAuth(int authId);
		bool AuthExists(int authId);
		bool Save();
	}
}
