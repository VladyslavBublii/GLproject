using Core.Models;
using System;
using System.Threading.Tasks;

namespace DAL.Interfaces;

public interface ICustomersRepository : IRepository<Customer>
{
    Task<Customer> GetByUserIdAsync(Guid userId);
}