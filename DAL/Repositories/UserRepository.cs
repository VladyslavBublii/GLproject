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
    public class UserRepository : IRepository<User>
    {
        private readonly StoreContext _storeContext;

        public UserRepository(StoreContext context)
        {
            _storeContext = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task CreateAsync(User user)
        {
            await _storeContext.Users.AddAsync(user);
        }

        public async Task DeleteAsync(Guid id)
        {
            var user = await _storeContext.Users.FindAsync(id);
            if (user != null)
            {
                _storeContext.Users.Remove(user);
                await _storeContext.SaveChangesAsync();
            }
        }

        public async Task DeleteRangeAsync(IEnumerable<User> users)
        {
            _storeContext.Users.RemoveRange(users);
            await _storeContext.SaveChangesAsync();
        }

        public async Task<User> GetAsync(Guid id)
        {
            return await _storeContext.Users.FindAsync(id);
        }

        public async Task<IEnumerable<User>> GetAsync(IEnumerable<Guid> ids)
        {
            return await _storeContext.Users.Where(c => ids.Contains(c.Id)).ToListAsync();
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _storeContext.Users.ToListAsync();
        }

        public async Task UpdateAsync(User user)
        {
            _storeContext.Entry(user).State = EntityState.Modified;
            await _storeContext.SaveChangesAsync();
        }

        public async Task<User> FindAsync(Guid id)
        {
            return await _storeContext.Users.Where(p => p.Id == id).FirstOrDefaultAsync();
        }
    }
}
