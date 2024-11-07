using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();

        Task<T> GetAsync(Guid id);

        Task<IEnumerable<T>> GetAsync(IEnumerable<Guid> ids);

        Task<T> FindAsync(Guid id);

        Task CreateAsync(T item);

        Task UpdateAsync(T item);

        Task DeleteAsync(Guid id);

        Task DeleteRangeAsync(IEnumerable<T> collection);
    }
}
