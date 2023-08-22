using BikeShopREST.Models;

namespace BikeShopREST.Interfaces
{
	public interface IAddressRepository
	{
		ICollection<Address> GetAddresses();
		Address GetAddress(int id);
		bool AddressExists(int id);
		Address GetAddressByUser(int userId);
		User GetUserByAddress(int addressId);
		bool Save();
	}
}
