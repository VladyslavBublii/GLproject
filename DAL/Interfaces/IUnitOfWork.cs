using Core.Models;
using System;

namespace DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<User> Users { get; }
        IRepository<Customer> Customers { get; }
        IRepository<Product> Products { get; }
        IRepository<Order> Orders { get; }
        IRepository<Cart> Carts { get; }
        ICustomersRepository CustomersRepository { get; }
        IOrdersRepository OrdersRepository { get; }
        IOrdersProductsRepository OrdersProducts { get; }
        void Save();
    }
}
