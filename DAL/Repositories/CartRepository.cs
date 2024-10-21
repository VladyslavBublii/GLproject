using Core.Models;
using DAL.Data;
using DAL.Interfaces;
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
			return _db.Carts.ToList();
		}

		public Cart Get(Guid id)
		{
			return _db.Carts.Find(id);
		}

        public IEnumerable<Cart> Get(IEnumerable<Guid> ids)
        {
            return _db.Carts.Where(c => ids.Contains(c.Id)).ToList();
        }

        public void Create(Cart product)
		{
			_db.Carts.Add(product);
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
			Cart ithem = _db.Carts.Find(productId);
			if (ithem != null)
			{
				_db.Carts.Remove(ithem);
			}
		}

        public void DeleteRange(IEnumerable<Cart> carts)
        {
            _db.Carts.RemoveRange(carts);
        }
    }
}
