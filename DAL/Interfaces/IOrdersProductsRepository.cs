using Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Interfaces;

public interface IOrdersProductsRepository
{
    Task AddOrderProductAsync(Guid ordersId, Guid productsId, int amount);

    Task AddRangeOrderProductAsync(ICollection<OrderProduct> orderProducts);

    Task DeleteOrderProductAsync(Guid ordersId, Guid productsId);

    Task<ICollection<OrderProduct>> GetOrderProductsByOrderIdAsync(Guid orderId);
}