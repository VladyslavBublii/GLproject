using Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IOrdersRepository
    {
        Task<IEnumerable<Order>> GetAllByUserIdAsync(Guid userId);

        Task<Guid> GetIdByUserIdAndTimeAsync(Guid userId, DateTime orderTime);
    }
}
