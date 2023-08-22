using BikeShopREST.Models;

namespace BikeShopREST.Interfaces
{
	public interface IReviewRepository
	{
		ICollection<Review> GetReviews();
		ICollection<Review> GetReviewsByBike(int bikeId);
		Review GetReview(int id);
		bool ReviewExists(int id);
		bool Save();

	}
}
