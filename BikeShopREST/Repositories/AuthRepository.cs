using BikeShopREST.Data;
using BikeShopREST.Interfaces;
using BikeShopREST.Models;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace BikeShopREST.Repositories
{
	public class AuthRepository : IAuthRepository
	{
		private readonly DataContext _context;
        public AuthRepository(DataContext context)
        {
			_context = context;
        }
        public bool Register(Auth auth)
		{
			_context.Add(auth);
			return Save();
		}
		public bool UpdateUserData(Auth auth)
		{
			_context.Update(auth);
			return Save();
		}
		public bool Save()
		{
			var saved =_context.SaveChanges();
			return saved > 0 ? true : false;
		}

		public bool AuthExists(int authId)
		{
			return _context.Auths.Any(a => a.Id == authId);
		}

		public Auth GetAuthByUser(int userId)
		{
			return _context.Auths.Where(u => u.Id == userId).FirstOrDefault();
		}

		public User GetUserByAuth(int authId)
		{
			return _context.Auths.Where(a => a.Id==authId).Select(a=>a.User).FirstOrDefault();
		}
	}
}
