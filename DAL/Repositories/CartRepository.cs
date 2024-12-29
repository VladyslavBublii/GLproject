using Core.Models;
using DAL.Data;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class CartRepository : IRepository<Cart>
    {
        private readonly DBContext _db;

        public CartRepository(DBContext context)
        {
            _db = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Cart>> GetAllAsync()
        {
            return await _db.Carts.ToListAsync();
        }

        public async Task<Cart> GetAsync(Guid id)
        {
            return await _db.Carts.FindAsync(id);
        }

        public async Task<IEnumerable<Cart>> GetAsync(IEnumerable<Guid> ids)
        {
            return await _db.Carts.Where(c => ids.Contains(c.Id)).ToListAsync();
        }

        public async Task CreateAsync(Cart cart)
        {
            await _db.Carts.AddAsync(cart);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Cart cart)
        {
            _db.Entry(cart).State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }

        public async Task<Cart> FindAsync(Guid id)
        {
            return await _db.Carts.Where(c => c.Id == id).FirstOrDefaultAsync();
        }

        public async Task DeleteAsync(Guid cartId)
        {
            var cart = await _db.Carts.FindAsync(cartId);
            if (cart != null)
            {
                _db.Carts.Remove(cart);
                await _db.SaveChangesAsync();
            }
        }

        public async Task DeleteRangeAsync(IEnumerable<Cart> carts)
        {
            _db.Carts.RemoveRange(carts);
            await _db.SaveChangesAsync();
        }
    }
}
