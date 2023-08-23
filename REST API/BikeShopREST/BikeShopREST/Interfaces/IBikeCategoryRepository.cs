using BikeShopREST.Models;

namespace BikeShopREST.Interfaces
{
	public interface IBikeCategoryRepository
	{
		ICollection<Bike> GetBikesByCategory(int categoryId);
		Category GetCategoryByBike(int bikeId);
		bool AssignBikeToCategory(int categoryId, int bikeId);
		bool Save();
	}
}
