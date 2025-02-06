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
    public class CustomerRepository : IRepository<Customer>, ICustomersRepository
    {
        private readonly StoreContext _storeContext;

        public CustomerRepository(StoreContext context)
        {
            _storeContext = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task CreateAsync(Customer customer)
        {
            await _storeContext.Customers.AddAsync(customer);
        }

        public async Task DeleteAsync(Guid id)
        {
            var customer = await _storeContext.Customers.FindAsync(id);
            if (customer != null)
            {
                _storeContext.Customers.Remove(customer);
                await _storeContext.SaveChangesAsync();
            }
        }

        public async Task DeleteRangeAsync(IEnumerable<Customer> customers)
        {
            _storeContext.Customers.RemoveRange(customers);
            await _storeContext.SaveChangesAsync();
        }

        public async Task<Customer> GetAsync(Guid id)
        {
            return await _storeContext.Customers.FindAsync(id);
        }

        public async Task<IEnumerable<Customer>> GetAsync(IEnumerable<Guid> ids)
        {
            return await _storeContext.Customers.Where(c => ids.Contains(c.Id)).ToListAsync();
        }

        public async Task<Customer> GetByUserIdAsync(Guid userId)
        {
            return await _storeContext.Customers.Where(p => p.UserId == userId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await _storeContext.Customers.ToListAsync();
        }

        public async Task UpdateAsync(Customer customer)
        {
            _storeContext.Entry(customer).State = EntityState.Modified;
            await _storeContext.SaveChangesAsync();
        }

        public async Task<Customer> FindAsync(Guid id)
        {
            return await _storeContext.Customers.Where(p => p.Id == id).FirstOrDefaultAsync();
        }
    }
}
