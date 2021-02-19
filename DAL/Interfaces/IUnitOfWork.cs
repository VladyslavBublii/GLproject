using DAL.Interfaces;
using DAL.Models;
using System;

namespace DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        Interfaces.IRepository<Product> Products { get; }
        Interfaces.IRepository<Order> Orders { get; }
        void Save();
    }
}