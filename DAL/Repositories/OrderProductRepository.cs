using Core.Models;
using DAL.Data;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories;

public class OrdersProductsRepository(StoreContext context) : IOrdersProductsRepository
{
    private readonly StoreContext _storeContext = context ?? throw new ArgumentNullException(nameof(context));

    public async Task AddOrderProductAsync(Guid ordersId, Guid productsId, int amount)
    {
        if (amount <= 0) throw new ArgumentException("Amount must be greater than zero.");

        var orderProduct = new OrderProduct { OrdersId = ordersId, ProductsId = productsId, NumberOfProduct = amount };
        await _storeContext.OrderProduct.AddAsync(orderProduct);
    }

    public async Task AddRangeOrderProductAsync(ICollection<OrderProduct> orderProducts)
    {
        if (orderProducts == null || !orderProducts.Any())
            throw new ArgumentException("Order products collection is empty or null.");

        await _storeContext.OrderProduct.AddRangeAsync(orderProducts);
    }

    public async Task DeleteOrderProductAsync(Guid ordersId, Guid productsId)
    {
        var orderProduct = await _storeContext.OrderProduct
            .FirstOrDefaultAsync(op => op.OrdersId == ordersId && op.ProductsId == productsId);

        if (orderProduct != null)
        {
            _storeContext.OrderProduct.Remove(orderProduct);
        }
    }

    public async Task<ICollection<OrderProduct>> GetOrderProductsByOrderIdAsync(Guid orderId)
    {
        return await _storeContext.OrderProduct
            .Where(item => item.OrdersId == orderId)
            .ToListAsync();
    }
}