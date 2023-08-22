using BikeShopREST.Data;
using BikeShopREST.Interfaces;
using BikeShopREST.Models;

namespace BikeShopREST.Repositories
{
	public class ReviewRepository : IReviewRepository
	{
		private readonly DataContext _context;
        public ReviewRepository(DataContext context)
        {
			_context = context;
        }
        public Review GetReview(int id)
		{
			return _context.Reviews.Where(r => r.Id == id).FirstOrDefault();
		}

		public ICollection<Review> GetReviews()
		{
			return _context.Reviews.OrderBy(r=> r.Id).ToList();
		}

		public ICollection<Review> GetReviewsByBike(int bikeId)
		{
			return _context.Reviews.Where(r => r.Bike.Id == bikeId).ToList();
		}

		public bool ReviewExists(int id)
		{
			return _context.Reviews.Any(r=>r.Id == id);
		}

		public bool Save()
		{
			var saved = _context.SaveChanges();
			return saved > 0 ? true : false;
		}
	}
}
