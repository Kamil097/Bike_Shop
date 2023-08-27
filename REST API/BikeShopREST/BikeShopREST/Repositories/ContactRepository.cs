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

		public bool CreateContact(Contact contact)
		{
			_context.Add(contact);
			return Save();
		}

		public Contact GetContact(int id)
		{
			return _context.Contacts.Where(c => c.Id == id).FirstOrDefault();
		}

		public Contact GetContactByUser (int userId)
		{
			return _context.Users.Where(c => c.Id == userId).Select(c=>c.Contact).FirstOrDefault();
		}

		public IEnumerable<Contact> GetContacts()
		{
			return _context.Contacts.OrderBy(c => c.Id).ToList();
		}

		public User GetUserByContact(int contactId)
		{
			return _context.Contacts.Where(c => c.Id == contactId).Select(c => c.User).FirstOrDefault();
		}

		public bool Save()
		{
			var saved = _context.SaveChanges();
			return saved > 0 ? true : false;
		}

		public bool UpdateContact(Contact contact)
		{
			_context.Update(contact);
			return Save();
		}
	}
}
