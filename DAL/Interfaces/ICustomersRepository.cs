using Core.Models;
using System;

namespace DAL.Interfaces
{
    public interface ICustomersRepository
    {
        Customer GetByUserId(Guid userId);
    }
}

