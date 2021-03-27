using Core.Models;
using DAL.Data;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Repositories
{
    public class CartRepository : IRepository<Cart>
    {
        private DBContext _db;

        public CartRepository(DBContext context)
        {
            _db = context;
        }

		public IEnumerable<Cart> GetAll()
		{
			return _db.Cart.ToList();
		}

		public Cart Get(Guid id)
		{
			return _db.Cart.Find(id);
		}

		public void Create(Cart product)
		{
			_db.Cart.Add(product);
		}

		public void Update(Cart item)
		{
			//_db.Cart(product).State = EntityState.Modified;
			throw new Exception();
		}

        public Cart Find(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid productId)
        {
			Cart ithem = _db.Cart.Find(productId);
			if (ithem != null)
			{
				_db.Cart.Remove(ithem);
			}
		}
    }
}
