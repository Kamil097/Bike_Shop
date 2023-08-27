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

		public bool AssignBikeToCategory(int categoryId, int bikeId)
		{
			var category = _context.Categories.Where(c => c.Id == categoryId).FirstOrDefault();
			var bike = _context.Bikes.Where(b => b.Id == bikeId).FirstOrDefault();

			var instance = new BikeCategory()
			{
				BikeId= bikeId,
				CategoryId= categoryId,
				Bike = bike,
				Category = category
			};
			_context.Add(instance);
			return Save();
		}

        public bool BikeCategoryExists(int bikeId, int categoryId)
        {
			return _context.BikeCategories.Any(bc => bc.CategoryId == categoryId && bc.BikeId == bikeId);
        }

        public BikeCategory GetBikeCategory(int bikeId, int categoryId)
        {
			return _context.BikeCategories.Where(bc => bc.BikeId == bikeId && bc.CategoryId == categoryId).FirstOrDefault();
        }

        public ICollection<Bike> GetBikesByCategory(int categoryId)
		{
			return _context.BikeCategories.Where(c => c.CategoryId == categoryId).Select(b => b.Bike).ToList();
		}
		
		public Category GetCategoryByBike(int bikeId)
		{
			return _context.BikeCategories.Where(b => b.BikeId == bikeId).Select(c => c.Category).FirstOrDefault();
		}

        public bool DeleteBikeCategory(BikeCategory bikeCategory)
        {
			_context.Remove(bikeCategory);
			return Save();

        }

        public bool Save()
		{
			var save = _context.SaveChanges();
			return save > 0 ? true : false;
		}
	}
}
