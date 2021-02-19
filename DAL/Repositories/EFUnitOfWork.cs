using System;
using System.Collections.Generic;
using System.Text;
using DAL.Data;
using DAL.Interfaces;
using DAL.Models;


namespace DAL.Repositories
{
	public class EFUnitOfWork : IUnitOfWork
	{
		private AppDbContext db;
		private ProductRepository productRepository;
		private OrderRepository orderRepository;

		public EFUnitOfWork(string connectionString)
		{
			db = new AppDbContext(connectionString);
		}
		public Interfaces.IRepository<Product> Products
		{
			get
			{
				if (productRepository == null)
					productRepository = new ProductRepository(db);
				return productRepository;
			}
		}

		public Interfaces.IRepository<Order> Orders
		{
			get
			{
				if (orderRepository == null)
					orderRepository = new OrderRepository(db);
				return orderRepository;
			}
		}

		public void Save()
		{
			db.SaveChanges();
		}

		private bool disposed = false;

		public virtual void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (disposing)
				{
					db.Dispose();
				}
				this.disposed = true;
			}
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
	}
}