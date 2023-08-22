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
		public bool Save()
		{
			var saved = _context.SaveChanges();
			return saved > 0 ? true : false;
		}

		public ICollection<Bike> GetBikesByUser(int userId)
		{
			return _context.BikeUsers.Where(b=>b.UserId == userId).Select(b=>b.Bike).ToList();
		}
	}
}
