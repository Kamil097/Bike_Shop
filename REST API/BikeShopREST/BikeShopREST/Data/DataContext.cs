using BikeShopREST.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace BikeShopREST.Data
{
	public class DataContext: DbContext
	{
        public DataContext(DbContextOptions<DataContext>options):base(options)
        {
            
        }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Address> Addresses{ get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<Bike> Bikes { get; set; }
		public DbSet<Auth> Auths { get; set; }
		public DbSet<Contact> Contacts{ get; set; }
		public DbSet<Review> Reviews { get; set; }
        public DbSet<BikeCategory> BikeCategories { get; set; }
		public DbSet<BikeUser> BikeUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<BikeCategory>()
				.HasKey(pc => new { pc.BikeId, pc.CategoryId });

			modelBuilder.Entity<BikeCategory>()
				.HasOne(p => p.Bike)
				.WithMany(pc => pc.BikeCategories)
				.HasForeignKey(c => c.BikeId);

			modelBuilder.Entity<BikeCategory>()
				.HasOne(p => p.Category)
				.WithMany(pc => pc.BikeCategories)
				.HasForeignKey(c => c.CategoryId);

			modelBuilder.Entity<BikeUser>()
				.HasKey(po => new { po.BikeId, po.UserId});

			modelBuilder.Entity<BikeUser>()
				.HasOne(p => p.Bike)
				.WithMany(po => po.BikeUsers)
				.HasForeignKey(c => c.BikeId);

			modelBuilder.Entity<BikeUser>()
				.HasOne(p => p.User)
				.WithMany(pc => pc.BikeUsers)
				.HasForeignKey(c => c.UserId);
			//-----------------------------
			modelBuilder.Entity<User>()
				.HasOne(u => u.Auth) 
				.WithOne(a => a.User)   
				.HasForeignKey<Auth>(a => a.UserId);

			modelBuilder.Entity<Auth>()
				.HasOne(a => a.User)
				.WithOne(u => u.Auth)
				.HasForeignKey<Auth>(a => a.UserId);

		}
	}
}
