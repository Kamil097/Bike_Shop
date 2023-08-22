using BikeShopREST.Models;

namespace BikeShopREST.Interfaces
{
	public interface IAuthRepository
	{
		Auth Register();
		Auth Login();
	}
}
