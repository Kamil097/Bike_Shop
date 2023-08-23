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
        public bool Register(Auth Auth)
		{
			_context.Add(Auth);
			return Save();
		}
		public bool Save()
		{
			var saved =_context.SaveChanges();
			return saved > 0 ? true : false;
		}
	}
}
