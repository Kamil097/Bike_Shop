﻿using BikeShopREST.Data;
using BikeShopREST.Interfaces;
using BikeShopREST.Models;

namespace BikeShopREST.Repositories
{
	public class CategoryRepository : ICategoryRepository
	{
		private readonly DataContext _context;
        public CategoryRepository(DataContext context)
        {
            _context = context;
        }
        public bool CategoryExists(int id)
		{
			return _context.Categories.Any(c=>c.Id == id);
		}

		public ICollection<Category> GetCategories()
		{
			return _context.Categories.OrderBy(c=>c.Id).ToList();	
		}

		public Category GetCategory(int id)
		{
			return _context.Categories.Where(c => c.Id == id).FirstOrDefault();
		}


		public bool Save()
		{
			var saved = _context.SaveChanges();
			return saved > 0? true: false;
		}
	}
}
