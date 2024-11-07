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
    public class OrderRepository : IRepository<Order>, IOrdersRepository
    {
        private readonly DBContext _db;

        public OrderRepository(DBContext context)
        {
            _db = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await _db.Orders.ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetAllByUserIdAsync(Guid userId)
        {
            return await _db.Orders.Where(item => item.UserId == userId).ToListAsync();
        }

        public async Task<Guid> GetIdByUserIdAndTimeAsync(Guid userId, DateTime orderTime)
        {
            var order = await _db.Orders
                .Where(item => item.UserId == userId && item.OrderTime == orderTime)
                .FirstOrDefaultAsync();

            return order?.Id ?? Guid.Empty;
        }

        public async Task<Order> GetAsync(Guid id)
        {
            return await _db.Orders.FindAsync(id);
        }

        public async Task<IEnumerable<Order>> GetAsync(IEnumerable<Guid> ids)
        {
            return await _db.Orders.Where(c => ids.Contains(c.Id)).ToListAsync();
        }

        public async Task CreateAsync(Order order)
        {
            await _db.Orders.AddAsync(order);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Order order)
        {
            _db.Entry(order).State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var order = await _db.Orders.FindAsync(id);
            if (order != null)
            {
                _db.Orders.Remove(order);
                await _db.SaveChangesAsync();
            }
        }

        public async Task DeleteRangeAsync(IEnumerable<Order> orders)
        {
            _db.Orders.RemoveRange(orders);
            await _db.SaveChangesAsync();
        }

        public async Task<Order> FindAsync(Guid id)
        {
            return await _db.Orders.Where(p => p.Id == id).FirstOrDefaultAsync();
        }
    }
}
