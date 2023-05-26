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
		private DBContext _db;

		public ProductRepository(DBContext context)
		{
			this._db = context;
		}

		public IEnumerable<Product> GetAll()
		{
			return _db.Products.ToList();
		}

		public Product Get(Guid id)
		{
			return _db.Products.Find(id);
		}

		public void Create(Product product)
		{
			product.Id = Guid.NewGuid();
			product.Orders = null;
            _db.Products.Add(product);
		}

		public void Update(Product product)
		{
            _db.Entry(product).State = EntityState.Modified;
		}

		public Product Find(Guid id)
		{
			var resultData = _db.Products.Where(p => p.Id == id).FirstOrDefault();
			return resultData;
		}

		public void Delete(Guid id)
		{
			Product product = _db.Products.Find(id);
			if (product != null)
			{
				_db.Products.Remove(product);
			}
		}
	}
}
