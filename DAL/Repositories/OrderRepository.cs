using Core.Models;
using DAL.Data;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace DAL.Repositories
{
	public class OrderRepository : IRepository<Order>
	{
		private DBContext db;

		public OrderRepository(DBContext context)
		{
			this.db = context;
		}

		public IEnumerable<Order> GetAll()
		{
			return db.Orders.Include(o => o.Product);
		}

		public Order Get(int id)
		{
			return db.Orders.Find(id);
		}

		public void Create(Order order)
		{
			db.Orders.Add(order);
		}

		public void Update(Order order)
		{
			db.Entry(order).State = EntityState.Modified;
		}
		//public IEnumerable<Order> Find(Func<Order, Boolean> predicate)
		//{
		//	return db.Orders.Include(o => o.Product).Where(predicate).ToList();
		//}
		public void Delete(int id)
		{
			Order order = db.Orders.Find(id);
			if (order != null)
				db.Orders.Remove(order);
		}
	}
}
