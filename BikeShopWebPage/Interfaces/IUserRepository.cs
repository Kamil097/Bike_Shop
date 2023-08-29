using BikeShopWebPage.Models;

namespace BikeShopWebPage.Interfaces
{
    public interface IUserRepository
    {
        User GetUser(int id);
        Address GetUserAddress(int userId);
        Contact GetUserContact(int userId);
        List<Review> GetUserReviews(int userId);
        List<Bike> GetBikesByUser(int userId);  
    }
}
