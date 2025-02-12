using Core.Models;
using DAL.Data;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories;

public class ProductRepository(StoreContext context) : Repository<Product>(context), IProductRepository
{
    private StoreContext StoreContext => Context as StoreContext;

    public async Task<IEnumerable<Product>> GetAsync(IEnumerable<Guid> ids)
    {
        // Преобразуем ids в список для многократного доступа
        var idList = ids.ToList();

        // Получаем уникальные продукты из базы данных
        var products = await StoreContext.Products
            .Where(p => idList.Contains(p.Id))
            .ToListAsync();

        // Восстанавливаем порядок и дубли
        var productsWithDuplicates = idList
            .Select(id => products.First(p => p.Id == id))
            .ToList();

        return productsWithDuplicates;
    }

    public new async Task CreateAsync(Product product)
    {
        product.Id = Guid.NewGuid();
        product.OrderProducts = null;
        await StoreContext.Products.AddAsync(product);
    }
}