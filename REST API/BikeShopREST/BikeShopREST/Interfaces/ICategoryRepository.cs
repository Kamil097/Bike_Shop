using BikeShopREST.Models;

namespace BikeShopREST.Interfaces
{
	public interface ICategoryRepository
	{
		ICollection<Category> GetCategories();
		Category GetCategory(int id);
		
		bool CategoryExists(int id);
		bool Save();
	}
}
