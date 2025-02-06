using Core.Models;
using DAL.Data;
using DAL.Interfaces;
using System;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private StoreContext _storeContext;
        private UserRepository _userRepository;
        private CustomerRepository _customerRepository;
        private ProductRepository _productRepository;
        private OrderRepository _orderRepository;
        private CartRepository _cartRepository;
        private OrdersProductsRepository _ordersProductsRepository;

        public UnitOfWork()
        {
            _storeContext = new StoreContext();
        }

        public IRepository<User> Users
        {
            get
            {
                if (_userRepository == null)
                {
                    _userRepository = new UserRepository(_storeContext);
                }
                return _userRepository;
            }
        }

        public IRepository<Customer> Customers
        {
            get
            {
                if (_customerRepository == null)
                {
                    _customerRepository = new CustomerRepository(_storeContext);
                }
                return _customerRepository;
            }
        }

        public ICustomersRepository CustomersRepository
        {
            get
            {
                if (_customerRepository == null)
                {
                    _customerRepository = new CustomerRepository(_storeContext);
                }
                return _customerRepository;
            }
        }

        public IRepository<Product> Products
        {
            get
            {
                if (_productRepository == null)
                {
                    _productRepository = new ProductRepository(_storeContext);
                }
                return _productRepository;
            }
        }

        public IRepository<Order> Orders
        {
            get
            {
                if (_orderRepository == null)
                {
                    _orderRepository = new OrderRepository(_storeContext);
                }
                return _orderRepository;
            }
        }

        public IOrdersRepository OrdersRepository
        {
            get
            {
                if (_orderRepository == null)
                {
                    _orderRepository = new OrderRepository(_storeContext);
                }
                return _orderRepository;
            }
        }

        public IRepository<Cart> Carts
        {
            get
            {
                if (_cartRepository == null)
                {
                    _cartRepository = new CartRepository(_storeContext);
                }
                return _cartRepository;
            }
        }

        public IOrdersProductsRepository OrdersProducts
        {
            get
            {
                if (_ordersProductsRepository == null)
                {
                    _ordersProductsRepository = new OrdersProductsRepository(_storeContext);
                }
                return _ordersProductsRepository;
            }
        }

        public async Task SaveAsync()
        {
            await _storeContext.SaveChangesAsync();
        }

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
}
