using BikeShopREST.Data;
using BikeShopREST.Interfaces;
using BikeShopREST.Models;

namespace BikeShopREST.Repositories
{
	public class BikeCategoryRepository : IBikeCategoryRepository
	{
		private readonly DataContext _context;
		public BikeCategoryRepository(DataContext context)
        {
			_context = context;
        }
        public ICollection<Bike> GetBikesByCategory(int categoryId)
		{
			return _context.BikeCategories.Where(c => c.CategoryId == categoryId).Select(b => b.Bike).ToList();
		}

		public Category GetCategoryByBike(int bikeId)
		{
			return _context.BikeCategories.Where(b => b.BikeId == bikeId).Select(c => c.Category).FirstOrDefault();
		}

		public bool Save()
		{
			var save = _context.SaveChanges();
			return save > 1 ? true : false;
		}
	}
}
