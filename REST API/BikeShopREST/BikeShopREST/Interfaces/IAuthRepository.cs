using BikeShopREST.Models;

namespace BikeShopREST.Interfaces
{
	public interface IAuthRepository
	{
		bool Register(Auth Auth);
	}
}
