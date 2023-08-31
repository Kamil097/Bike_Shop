using BikeShopWebPage.Models;

namespace BikeShopWebPage.Interfaces
{
    public interface IBikeRepository
    {
        List<Bike> GetAllBikes();
        Bike GetBikeById(int bikeId);
        List<Review> GetReviewsByBike(int bikeId);  
    }
}
