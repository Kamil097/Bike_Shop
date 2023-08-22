using BikeShopREST.Data;
using BikeShopREST.Models;

namespace BikeShopREST
{
	public class Seed
	{
		private readonly DataContext _context;
		public Seed(DataContext context)
		{
			_context = context;
		}
		public void SeedDataContext()
		{
			if (!_context.Users.Any())
			{
				var users = new List<User>
				{
					new User
					{
						FirstName = "John",
						LastName = "Doe",
						Contact = new Contact { PhoneNo = 123456789, Email = "john@example.com" },
						Address = new Address { Country = "USA", City = "New York", Street = "123 Main St", PostCode = "10001" },
						Auth = new Auth { Username = "johndoe", PasswordHash = "hashedpassword" }
					},
					new User
					{
						FirstName = "Jane",
						LastName = "Smith",
						Contact = new Contact { PhoneNo = 987654321, Email = "jane@example.com" },
						Address = new Address { Country = "Canada", City = "Toronto", Street = "456 Elm St", PostCode = "M5V 2M5" },
						Auth = new Auth { Username = "janesmith", PasswordHash = "hashedpassword" }
					}
				};

					_context.Users.AddRange(users);

				var bikes = new List<Bike>
				{
					new Bike
					{
						Name = "Mountain Bike",
						Year = 2022,
						Price = 1200.0f,
						Rating = 4.5f,
						BikeCategories = new List<BikeCategory>
						{
							new BikeCategory { Category = new Category { Name = "Mountain", Description="Fun" } }
						},
						BikeUsers = new List<BikeUser>
						{
							new BikeUser { User = users[0] }
						},
						Reviews = new List<Review>
						{
							new Review { Title = "Great Bike", Text = "I love this mountain bike!", Rating = 5, User = users[0] }
						}
					}
				};

					_context.Bikes.AddRange(bikes);

					_context.SaveChanges();
				}
		}
	}
}
