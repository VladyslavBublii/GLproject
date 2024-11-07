using Core.Models;
using System;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface ICustomersRepository
    {
        Task<Customer> GetByUserIdAsync(Guid userId);
    }
}
