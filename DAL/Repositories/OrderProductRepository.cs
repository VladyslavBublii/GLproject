using Core.Models;
using DAL.Data;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace DAL.Repositories
{
    public class OrdersProductsRepository : IOrdersProductsRepository
    {
        private DBContext _db;

        public OrdersProductsRepository(DBContext context)
        {
            _db = context;
        }

        public void AddOrderProduct(Guid ordersId, Guid productsId, int amount)
        {
            var orderProduct = new OrderProduct { OrdersId = ordersId, ProductsId = productsId, NumberOfProduct = amount };
            _db.OrderProduct.Add(orderProduct);
        }

        public void AddRangeOrderProduct(ICollection<OrderProduct> orderProducts)
        {
            _db.OrderProduct.AddRange(orderProducts);
        }

        public void DeleteOrderProduct(Guid ordersId, Guid productsId)
        {
            var orderProduct = new OrderProduct { OrdersId = ordersId, ProductsId = productsId };
            _db.OrderProduct.Remove(orderProduct);
        }

        public ICollection<OrderProduct> GetOrderProductsByOrderId(Guid orderId)
        {
            return _db.OrderProduct.Where(item => item.OrdersId == orderId).ToList();
        }
    }
}
