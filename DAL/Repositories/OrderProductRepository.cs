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
    public class OrdersProductsRepository : IOrdersProductsRepository
    {
        private readonly DBContext _db;

        public OrdersProductsRepository(DBContext context)
        {
            _db = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task AddOrderProductAsync(Guid ordersId, Guid productsId, int amount)
        {
            if (amount <= 0) throw new ArgumentException("Amount must be greater than zero.");

            var orderProduct = new OrderProduct { OrdersId = ordersId, ProductsId = productsId, NumberOfProduct = amount };
            await _db.OrderProduct.AddAsync(orderProduct);
            await _db.SaveChangesAsync();
        }

        public async Task AddRangeOrderProductAsync(ICollection<OrderProduct> orderProducts)
        {
            if (orderProducts == null || !orderProducts.Any())
                throw new ArgumentException("Order products collection is empty or null.");

            await _db.OrderProduct.AddRangeAsync(orderProducts);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteOrderProductAsync(Guid ordersId, Guid productsId)
        {
            var orderProduct = await _db.OrderProduct
                .FirstOrDefaultAsync(op => op.OrdersId == ordersId && op.ProductsId == productsId);

            if (orderProduct != null)
            {
                _db.OrderProduct.Remove(orderProduct);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<ICollection<OrderProduct>> GetOrderProductsByOrderIdAsync(Guid orderId)
        {
            return await _db.OrderProduct
                .Where(item => item.OrdersId == orderId)
                .ToListAsync();
        }
    }
}
