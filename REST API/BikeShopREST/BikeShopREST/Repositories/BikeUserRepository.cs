using BikeShopREST.Data;
using BikeShopREST.Interfaces;
using BikeShopREST.Models;
using Microsoft.EntityFrameworkCore;

namespace BikeShopREST.Repositories
{
    public class BikeUserRepository : IbikeUserRepository
    {
        private readonly DataContext _context;
        public BikeUserRepository(DataContext context)
        {
            _context = context;
        }
        public bool AssignBikeToUser(int userId, int bikeId)
        {
            var bike = _context.Bikes.Where(b => b.Id == bikeId).FirstOrDefault();
            var user = _context.Users.Where(u => u.Id == userId).FirstOrDefault();
            var instance = new BikeUser()
            {
                Bike = bike,
                User = user
            };
            _context.Add(instance);
            return Save();
        }

        public bool BikeUserExists(int bikeId, int userId)
        {
            return _context.BikeUsers.Any(bc => bc.BikeId == bikeId && bc.UserId == userId);
        }

        public bool DeleteBikeFromUser(BikeUser bikeUser)
        {
            _context.Remove(bikeUser);
            return Save();
        }

        public ICollection<Bike> GetBikesByUser(int userId)
        {
            return _context.BikeUsers.Where(b => b.UserId == userId).Select(b => b.Bike).ToList();
        }

        public BikeUser GetBikeUser(int bikeId, int userId)
        {
            return _context.BikeUsers.Where(bc => bc.BikeId == bikeId && bc.UserId == userId).FirstOrDefault();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
