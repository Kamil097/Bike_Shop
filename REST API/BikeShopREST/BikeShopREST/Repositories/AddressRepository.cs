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

		public bool CreateAddress(Address address)
		{
			_context.Add(address);
			return Save();
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

		public ICollection<User> GetUserByAddress(int addressId)
		{
			return _context.Addresses.Where(a => a.Id == addressId).Select(a => a.User).FirstOrDefault();
		}
		public bool UpdateAddress(Address address)
		{
			_context.Update(address);
			return Save();
		}
		public bool Save()
		{
			var saved = _context.SaveChanges();
			return saved > 0 ? true : false;
		}

        public bool DeleteAddress(Address address)
        {
			_context.Remove(address);
			return Save();
        }
    }
}
