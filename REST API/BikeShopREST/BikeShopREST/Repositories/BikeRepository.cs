using BikeShopREST.Data;
using BikeShopREST.Interfaces;
using BikeShopREST.Models;

namespace BikeShopREST.Repositories
{
	public class BikeRepository : IBikeRepository
	{
		private readonly DataContext _context;
        public BikeRepository(DataContext context)
        {
			_context = context;    
        }
        public bool BikeExists(int id)
		{
			return _context.Bikes.Any(b=>b.Id == id);
		}

		public ICollection<Bike> GetBikes()
		{
			return _context.Bikes.OrderBy(b => b.Id).ToList();
		}

		public Bike GetBike(int id)
		{
			return _context.Bikes.Where(b => b.Id == id).FirstOrDefault();
		}

		public bool Save()
		{
			var saved = _context.SaveChanges();
			return saved > 0 ? true : false;
		}
	}
}
