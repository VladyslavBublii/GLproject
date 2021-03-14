using Core.Models;
using DAL.Data;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

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
			return _db.Cart;
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

		public void Delete(int id)
		{
			var product = _db.Cart.Find(id);
			if (product != null)
				_db.Cart.Remove(product);
		}

        public Cart Find(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
