using System;
using System.Threading.Tasks;

namespace DAL.Interfaces;

public interface IUnitOfWork : IDisposable
{
    ICartRepository Carts { get; }
    
    ICustomersRepository Customers { get; }
    
    IOrdersProductsRepository OrdersProducts { get; }
    
    IOrdersRepository Orders { get; }
    
    IProductRepository Products { get; }
    
    IUserRepository Users { get; }

    Task SaveAsync();
}