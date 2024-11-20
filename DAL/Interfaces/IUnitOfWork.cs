using Core.Models;
using System;
using System.Threading.Tasks;

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

        Task SaveAsync();
    }
}
