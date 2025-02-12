using Core.Models;
using DAL.Data;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories;

public class CustomerRepository(StoreContext context) : Repository<Customer>(context), ICustomersRepository
{
    private StoreContext StoreContext => Context as StoreContext;

    public async Task<Customer> GetByUserIdAsync(Guid userId) =>
        await StoreContext.Customers.Where(p => p.UserId == userId).FirstOrDefaultAsync();
}