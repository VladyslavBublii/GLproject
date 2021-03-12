using Core.Models;
using DAL.Data;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Repositories
{
	public class OrderRepository : IRepository<Order>
	{
		private DBContext _db;

		public OrderRepository(DBContext context)
		{
			_db = context;
		}

		public IEnumerable<Order> GetAll()
		{
			return _db.Orders.Include(o => o.Products);
		}

		public Order Get(Guid id)
		{
			return _db.Orders.Find(id);
		}

		public void Create(Order order)
		{
			_db.Orders.Add(order);
		}

		public void Update(Order order)
		{
			_db.Entry(order).State = EntityState.Modified;
		}

		//public IEnumerable<Order> Find(Func<Order, Boolean> predicate)
		//{
		//	return db.Orders.Include(o => o.Product).Where(predicate).ToList();
		//}

		public void Delete(Guid id)
		{
			Order order = _db.Orders.Find(id);
			if (order != null)
			{
				db.Orders.Remove(order);
			}
		}

		public Order Find(Guid id)
		{
			var resultData = db.Orders.Where(p => p.Id == id).FirstOrDefault();
			return resultData;
		}
	}
}
