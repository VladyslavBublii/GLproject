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
        private readonly DBContext db;

        public CustomerRepository(DBContext context)
        {
            db = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task CreateAsync(Customer customer)
        {
            await db.Customers.AddAsync(customer);
            await db.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var customer = await db.Customers.FindAsync(id);
            if (customer != null)
            {
                db.Customers.Remove(customer);
                await db.SaveChangesAsync();
            }
        }

        public async Task DeleteRangeAsync(IEnumerable<Customer> customers)
        {
            db.Customers.RemoveRange(customers);
            await db.SaveChangesAsync();
        }

        public async Task<Customer> GetAsync(Guid id)
        {
            return await db.Customers.FindAsync(id);
        }

        public async Task<IEnumerable<Customer>> GetAsync(IEnumerable<Guid> ids)
        {
            return await db.Customers.Where(c => ids.Contains(c.Id)).ToListAsync();
        }

        public async Task<Customer> GetByUserIdAsync(Guid userId)
        {
            return await db.Customers.Where(p => p.UserId == userId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await db.Customers.ToListAsync();
        }

        public async Task UpdateAsync(Customer customer)
        {
            db.Entry(customer).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }

        public async Task<Customer> FindAsync(Guid id)
        {
            return await db.Customers.Where(p => p.Id == id).FirstOrDefaultAsync();
        }
    }
}
