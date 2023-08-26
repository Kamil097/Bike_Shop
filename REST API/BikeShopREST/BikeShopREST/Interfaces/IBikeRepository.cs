using BikeShopREST.Models;

namespace BikeShopREST.Interfaces
{
	public interface IBikeRepository
	{
		ICollection<Bike> GetBikes();
		Bike GetBike(int id);
		bool CreateBike(Bike Bike);
		bool UpdateBike(Bike Bike);	
		bool BikeExists(int id);
		bool Save();
	}
}
