﻿using Core.Models;
using DAL.Data;
using DAL.Interfaces;
using System;

namespace DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private DBContext _db;
        private UserRepository userRepository;
        private CustomerRepository customerRepository;
        private ProductRepository productRepository;
        private OrderRepository orderRepository;
        private CartRepository cartRepository;
        private OrdersProductsRepository ordersProductsRepository;

        public UnitOfWork()
        {
            _db = new DBContext();
        }

        public IRepository<User> Users
        {
            get
            {
                if (userRepository == null)
                {
                    userRepository = new UserRepository(_db);
                }
                return userRepository;
            }
        }

        public IRepository<Customer> Customers
        {
            get
            {
                if (customerRepository == null)
                {
                    customerRepository = new CustomerRepository(_db);
                }
                return customerRepository;
            }
        }

        public ICustomersRepository CustomersRepository
        {
            get
            {
                if (customerRepository == null)
                {
                    customerRepository = new CustomerRepository(_db);
                }
                return customerRepository;
            }
        }

        public IRepository<Product> Products
        {
            get
            {
                if (productRepository == null)
                {
                    productRepository = new ProductRepository(_db);
                }
                return productRepository;
            }
        }

        public IRepository<Order> Orders
        {
            get
            {
                if (orderRepository == null)
                {
                    orderRepository = new OrderRepository(_db);
                }
                return orderRepository;
            }
        }

        public IOrdersRepository OrdersRepository
        {
            get
            {
                if (orderRepository == null)
                {
                    orderRepository = new OrderRepository(_db);
                }
                return orderRepository;
            }
        }

        public IRepository<Cart> Carts
        {
            get
            {
                if (cartRepository == null)
                {
                    cartRepository = new CartRepository(_db);
                }
                return cartRepository;
            }
        }

        public IOrdersProductsRepository OrdersProducts
        {
            get
            {
                if (ordersProductsRepository == null)
                {
                    ordersProductsRepository = new OrdersProductsRepository(_db);
                }
                return ordersProductsRepository;
            }
        }

        public void Save()
        {
            int t = _db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
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
