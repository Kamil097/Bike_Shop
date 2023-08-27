using BikeShopREST.Models;

namespace BikeShopREST.Interfaces
{
	public interface IAddressRepository
	{
		ICollection<Address> GetAddresses();
		Address GetAddress(int id);
		bool AddressExists(int id);
		Address GetAddressByUser(int userId);
		ICollection<User> GetUserByAddress(int addressId);
		bool UpdateAddress(Address address);
		bool DeleteAddress(Address address);
		bool CreateAddress(Address address);
		bool Save();
	}
}
