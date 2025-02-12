using System;
using System.Threading.Tasks;
using DAL.Data;
using DAL.Interfaces;

namespace DAL.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly StoreContext _storeContext;
    
    public ICartRepository Carts { get; }
    public ICustomersRepository Customers { get; }
    public IOrdersProductsRepository OrdersProducts { get; }
    public IOrdersRepository Orders { get; }
    public IProductRepository Products { get; }
    public IUserRepository Users { get; }
    
    public UnitOfWork()
    {
        _storeContext = new StoreContext();
        Carts = new CartRepository(_storeContext);
        Customers = new CustomerRepository(_storeContext);
        OrdersProducts = new OrdersProductsRepository(_storeContext);
        Orders = new OrderRepository(_storeContext);
        Products = new ProductRepository(_storeContext);
        Users = new UserRepository(_storeContext);
    }

    public async Task SaveAsync() => await _storeContext.SaveChangesAsync();

    private bool disposed = false;

    public virtual void Dispose(bool disposing)
    {
        if (!this.disposed)
        {
            if (disposing)
            {
                _storeContext.Dispose();
            }
            this.disposed = true;
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}