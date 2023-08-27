using BikeShopREST.Models;

namespace BikeShopREST.Interfaces
{
    public interface IbikeUserRepository
    {
        bool AssignBikeToUser(int userId, int bikeId);
        bool DeleteBikeFromUser(BikeUser bikeUser);
        bool BikeUserExists(int bikeId, int userId);
        BikeUser GetBikeUser(int bikeId, int userId);
        ICollection<Bike> GetBikesByUser(int userId);
        bool Save();
    }
}
