using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Models;

namespace DAL.Interfaces;

public interface IProductRepository : IRepository<Product>
{
    Task<IEnumerable<Product>> GetAsync(IEnumerable<Guid> ids);
}