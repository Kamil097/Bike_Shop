using BikeShopREST.Data;
using BikeShopREST.Interfaces;
using BikeShopREST.Models;

namespace BikeShopREST.Repositories
{
	public class UserRepository : IUserRepository
	{
		private readonly DataContext _context;
		public UserRepository(DataContext context)
        {
			_context = context;
        }
        public ICollection<User> GetUsers()
		{
			return _context.Users.OrderBy(x => x.Id).ToList();	
		}

		public User GetUser(int id)
		{
			return _context.Users.Where(u => u.Id == id).FirstOrDefault();
		}
		public bool UserExists(int id)
		{
			return _context.Users.Any(u => u.Id == id);	
		}
		public ICollection<Bike> GetBikesByUser(int userId)
		{
			return _context.BikeUsers.Where(b => b.UserId == userId).Select(b => b.Bike).ToList();
		}

		public bool CreateUser(User user)
		{
			_context.Add(user);
			return Save();
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
		public bool Save()
		{
			var saved = _context.SaveChanges();
			return saved > 0 ? true : false;
		}

		public bool UpdateUser(User User)
		{
			_context.Update(User);
			return Save();	
		}
	}
}
