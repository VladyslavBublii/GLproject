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
        private readonly StoreContext _storeContext;

        public CartRepository(StoreContext context)
        {
            _storeContext = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Cart>> GetAllAsync()
        {
            return await _storeContext.Carts.ToListAsync();
        }

        public async Task<Cart> GetAsync(Guid id)
        {
            return await _storeContext.Carts.FindAsync(id);
        }

        public async Task<IEnumerable<Cart>> GetAsync(IEnumerable<Guid> ids)
        {
            return await _storeContext.Carts.Where(c => ids.Contains(c.Id)).ToListAsync();
        }

        public async Task CreateAsync(Cart cart)
        {
            await _storeContext.Carts.AddAsync(cart);
            await _storeContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Cart cart)
        {
            _storeContext.Entry(cart).State = EntityState.Modified;
            await _storeContext.SaveChangesAsync();
        }

        public async Task<Cart> FindAsync(Guid id)
        {
            return await _storeContext.Carts.Where(c => c.Id == id).FirstOrDefaultAsync();
        }

        public async Task DeleteAsync(Guid cartId)
        {
            var cart = await _storeContext.Carts.FindAsync(cartId);
            if (cart != null)
            {
                _storeContext.Carts.Remove(cart);
                await _storeContext.SaveChangesAsync();
            }
        }

        public async Task DeleteRangeAsync(IEnumerable<Cart> carts)
        {
            _storeContext.Carts.RemoveRange(carts);
            await _storeContext.SaveChangesAsync();
        }
    }
}
