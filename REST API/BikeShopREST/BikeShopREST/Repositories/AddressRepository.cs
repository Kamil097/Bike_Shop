using BikeShopREST.Data;
using BikeShopREST.Interfaces;
using BikeShopREST.Models;

namespace BikeShopREST.Repositories
{
	public class AddressRepository : IAddressRepository
	{
		private readonly DataContext _context;
        public AddressRepository(DataContext context)
        {
			_context = context; 
        }
        public bool AddressExists(int id)
		{
			return _context.Addresses.Any(a => a.Id == id);
		}

		public Address GetAddress(int id)
		{
			return _context.Addresses.Where(a => a.Id == id).FirstOrDefault();
		}

		public Address GetAddressByUser(int userId)
		{
			return _context.Users.Where(u => u.Id == userId).Select(u => u.Address).FirstOrDefault();
		}

		public ICollection<Address> GetAddresses()
		{
			return _context.Addresses.OrderBy(a=>a.Id).ToList();
		}

		public User GetUserByAddress(int addressId)
		{
			return _context.Addresses.Where(a => a.Id == addressId).Select(a => a.User).FirstOrDefault();
		}

		public bool Save()
		{
			var saved = _context.SaveChanges();
			return saved > 0 ? true : false;
		}
	}
}
