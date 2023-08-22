using BikeShopREST.Models;

namespace BikeShopREST.Interfaces
{
	public interface IContactRepository
	{
		IEnumerable<Contact> GetContacts();
		Contact GetContact(int id);
		Contact GetContactByUser(int userId);
		bool ContactExists(int id);
		bool Save();
	}
}
