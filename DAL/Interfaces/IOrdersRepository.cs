using Core.Models;
using System;
using System.Collections.Generic;

namespace DAL.Interfaces
{
    public interface IOrdersRepository
    {
        IEnumerable<Order> GetAllByUserId(Guid userId);
        Guid GetIdByUserIdAndTime(Guid userId, DateTime orderTime);
    }
}
