using BikeShopREST.Models;

namespace BikeShopREST.Interfaces
{
	public interface IReviewRepository
	{
		ICollection<Review> GetReviews();
		ICollection<Review> GetReviewsByBike(int bikeId);
		ICollection<Review> GetReviewsByUser(int userId);
		Review GetReview(int id);
		bool CreateReview(Review review);
		bool UpdateReview(Review Review);
		bool DeleteReview(Review review);
		bool ReviewExists(int id);
		bool Save();

	}
}
