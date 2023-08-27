using BikeShopREST.Models;

namespace BikeShopREST.Interfaces
{
	public interface IBikeRepository
	{
		ICollection<Bike> GetBikes();
		Bike GetBike(int id);
		bool CreateBike(Bike bike);
		bool UpdateBike(Bike bike);	
		bool DeleteBike(Bike bike);
		bool BikeExists(int id);
		bool Save();
	}
}
