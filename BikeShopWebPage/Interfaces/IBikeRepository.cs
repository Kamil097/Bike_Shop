using BikeShopWebPage.Models;

namespace BikeShopWebPage.Interfaces
{
    public interface IBikeRepository
    {
        List<Bike> GetAllBikes();
    }
}
