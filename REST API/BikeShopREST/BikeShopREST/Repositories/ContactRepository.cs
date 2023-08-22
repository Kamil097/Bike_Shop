using BikeShopREST.Data;
using BikeShopREST.Interfaces;
using BikeShopREST.Models;

namespace BikeShopREST.Repositories
{
	public class ContactRepository : IContactRepository
	{
		private readonly DataContext _context;
        public ContactRepository(DataContext context)
        {
			_context = context;
        }
        public bool ContactExists(int id)
		{
			return _context.Contacts.Any(c=>c.Id == id);
		}

		public Contact GetContact(int id)
		{
			return _context.Contacts.Where(c => c.Id == id).FirstOrDefault();
		}

		public Contact GetContactByUser(int userId)
		{
			return _context.Contacts.Where(c => c.User.Id == userId).FirstOrDefault();
		}

		public IEnumerable<Contact> GetContacts()
		{
			return _context.Contacts.OrderBy(c => c.Id).ToList();
		}

		public bool Save()
		{
			var saved = _context.SaveChanges();
			return saved > 0 ? true : false;
		}
	}
}
