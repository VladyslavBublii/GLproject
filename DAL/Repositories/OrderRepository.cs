using Core.Models;
using DAL.Data;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories;

public class OrderRepository(StoreContext context) : Repository<Order>(context), IOrdersRepository
{
    private StoreContext StoreContext => Context as StoreContext;

    public async Task<IEnumerable<Order>> GetAllByUserIdAsync(Guid userId)
    {
        return await StoreContext.Orders.Where(item => item.UserId == userId).ToListAsync();
    }

    public async Task<Guid> GetIdByUserIdAndTimeAsync(Guid userId, DateTime orderTime)
    {
        var order = await StoreContext.Orders
            .Where(item => item.UserId == userId && item.OrderTime == orderTime)
            .FirstOrDefaultAsync();

        return order?.Id ?? Guid.Empty;
    }
}