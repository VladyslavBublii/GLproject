using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using DAL.Models;
using DAL.Interfaces;
using System.Data.Entity;
using DAL.Data;

namespace DAL.Repositories
{
	public class ProductRepository : Interfaces.IRepository<Product>
	{
		private AppDbContext db;

		public ProductRepository(AppDbContext context)
		{
			this.db = context;
		}

		public IEnumerable<Product> GetAll()
		{
			return db.Products;
		}

		public Product Get(int id)
		{
			return db.Products.Find(id);
		}

		public void Create(Product product)
		{
			db.Products.Add(product);
		}

		public void Update(Product product)
		{
			db.Entry(product).State = EntityState.Modified;
		}

		public IEnumerable<Product> Find(Func<Product, Boolean> predicate)
		{
			return db.Products.Where(predicate).ToList();
		}

		public void Delete(int id)
		{
			Product product = db.Products.Find(id);
			if (product != null)
				db.Products.Remove(product);
		}
	}
}