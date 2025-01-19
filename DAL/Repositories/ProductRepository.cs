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
    public class ProductRepository : IRepository<Product>
    {
        private readonly DBContext _db;

        public ProductRepository(DBContext context)
        {
            _db = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _db.Products.ToListAsync();
        }

        public async Task<Product> GetAsync(Guid id)
        {
            return await _db.Products.FindAsync(id);
        }

        public async Task<IEnumerable<Product>> GetAsync(IEnumerable<Guid> ids)
        {
            // Преобразуем ids в список для многократного доступа
            var idList = ids.ToList();

            // Получаем уникальные продукты из базы данных
            var products = await _db.Products
                .Where(p => idList.Contains(p.Id))
                .ToListAsync();

            // Восстанавливаем порядок и дубли
            var productsWithDuplicates = idList
                .Select(id => products.First(p => p.Id == id))
                .ToList();

            return productsWithDuplicates;
        }

        public async Task CreateAsync(Product product)
        {
            product.Id = Guid.NewGuid();
            product.OrderProducts = null;
            await _db.Products.AddAsync(product);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product product)
        {
            _db.Entry(product).State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }

        public async Task<Product> FindAsync(Guid id)
        {
            return await _db.Products.Where(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var product = await _db.Products.FindAsync(id);
            if (product != null)
            {
                _db.Products.Remove(product);
                await _db.SaveChangesAsync();
            }
        }

        public async Task DeleteRangeAsync(IEnumerable<Product> products)
        {
            _db.Products.RemoveRange(products);
            await _db.SaveChangesAsync();
        }
    }
}
