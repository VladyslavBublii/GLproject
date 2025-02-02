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
        private readonly DBContext _db;

        public UserRepository(DBContext context)
        {
            _db = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task CreateAsync(User user)
        {
            await _db.Users.AddAsync(user);
        }

        public async Task DeleteAsync(Guid id)
        {
            var user = await _db.Users.FindAsync(id);
            if (user != null)
            {
                _db.Users.Remove(user);
                await _db.SaveChangesAsync();
            }
        }

        public async Task DeleteRangeAsync(IEnumerable<User> users)
        {
            _db.Users.RemoveRange(users);
            await _db.SaveChangesAsync();
        }

        public async Task<User> GetAsync(Guid id)
        {
            return await _db.Users.FindAsync(id);
        }

        public async Task<IEnumerable<User>> GetAsync(IEnumerable<Guid> ids)
        {
            return await _db.Users.Where(c => ids.Contains(c.Id)).ToListAsync();
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _db.Users.ToListAsync();
        }

        public async Task UpdateAsync(User user)
        {
            _db.Entry(user).State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }

        public async Task<User> FindAsync(Guid id)
        {
            return await _db.Users.Where(p => p.Id == id).FirstOrDefaultAsync();
        }
    }
}
