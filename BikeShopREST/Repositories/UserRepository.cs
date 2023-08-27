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

		public bool CreateUser(User user)
		{
			_context.Add(user);
			return Save();
		}
		public bool Save()
		{
			var saved = _context.SaveChanges();
			return saved > 0 ? true : false;
		}

		public bool UpdateUser(User user)
		{
			_context.Update(user);
			return Save();	
		}

        public bool DeleteUser(User user)
        {
			
			var userContact = _context.Contacts.Where(c=>c.UserId==user.Id).FirstOrDefault();
			var userAuth = _context.Auths.Where(a=>a.UserId==user.Id).FirstOrDefault();
			var userReviews = _context.Reviews.Where(r => r.User.Id == user.Id).ToList();
			_context.Remove(userAuth);
			_context.Remove(userContact);

			foreach(var review in userReviews)
				_context.Remove(review);

			_context.Remove(user);
			
            return Save();
        }
    }
}
