using BikeShopREST.Models;

namespace BikeShopREST.Interfaces
{
	public interface IBikeCategoryRepository
	{
		ICollection<Bike> GetBikesByCategory(int categoryId);
		Category GetCategoryByBike(int bikeId);
		BikeCategory GetBikeCategory(int bikeId, int categoryId);	
		bool AssignBikeToCategory(int categoryId, int bikeId);
		bool DeleteBikeCategory(BikeCategory bikecategory);
		bool BikeCategoryExists(int bikeId, int categoryId);
		bool Save();
	}
}
