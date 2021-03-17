using Core.Models;
using DAL.Data;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Repositories
{
	public class ProductRepository : IRepository<Product>
	{
		private DBContext db;

		public ProductRepository(DBContext context)
		{
			this.db = context;
		}

		public IEnumerable<Product> GetAll()
		{
			return db.Products.ToList();
		}

		public Product Get(Guid id)
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

		//public IEnumerable<Product> Find(Func<Product, Boolean> predicate)
		//{
			//return db.Products.Where(predicate).ToList();
		//}

		public Product Find(Guid id)
		{
			var resultData = db.Products.Where(p => p.Id == id).FirstOrDefault();
			return resultData;
		}

		public void Delete(Guid id)
		{
			Product product = db.Products.Find(id);
			if (product != null)
			{
				db.Products.Remove(product);
			}
		}
	}
}
