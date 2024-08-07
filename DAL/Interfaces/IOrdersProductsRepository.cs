using Core.Models;
using System;
using System.Collections.Generic;

namespace DAL.Interfaces
{
    public interface IOrdersProductsRepository
    {
        void AddOrderProduct(Guid ordersId, Guid productsId, int amount);
        void AddRangeOrderProduct(ICollection<OrderProduct> orderProducts);
        void DeleteOrderProduct(Guid ordersId, Guid productsId);
    }
}
