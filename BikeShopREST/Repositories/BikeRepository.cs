﻿using BikeShopREST.Data;
using BikeShopREST.Interfaces;
using BikeShopREST.Models;

namespace BikeShopREST.Repositories
{
	public class BikeRepository : IBikeRepository
	{
		private readonly DataContext _context;
        public BikeRepository(DataContext context)
        {
			_context = context;    
        }
        public bool BikeExists(int id)
		{
			return _context.Bikes.Any(b=>b.Id == id);
		}

		public ICollection<Bike> GetBikes()
		{
			return _context.Bikes.OrderBy(b => b.Id).ToList();
		}

		public Bike GetBike(int id)
		{
			return _context.Bikes.Where(b => b.Id == id).FirstOrDefault();
		}
		public bool CreateBike(Bike bike)
		{
			_context.Add(bike);
			return Save();
		}
		public bool Save()
		{
			var saved = _context.SaveChanges();
			return saved > 0 ? true : false;
		}

		public bool UpdateBike(Bike bike)
		{
			_context.Update(bike);
			return Save();
		}

        public bool DeleteBike(Bike bike)
        {
			_context.Remove(bike);
			return Save();
        }
    }
}
