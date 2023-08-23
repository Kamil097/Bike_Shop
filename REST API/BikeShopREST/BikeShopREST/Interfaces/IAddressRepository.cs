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
		bool CreateAddress(Address address);
		bool Save();
	}
}
