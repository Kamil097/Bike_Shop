using BikeShopREST.Models;

namespace BikeShopREST.Interfaces
{
	public interface ICategoryRepository
	{
		ICollection<Category> GetCategories();
		Category GetCategory(int id);
		bool CreateCategory(Category category);
		bool UpdateCategory(Category Category);
		bool CategoryExists(int id);
		bool Save();
	}
}
