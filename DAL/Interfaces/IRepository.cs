using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DAL.Interfaces;

public interface IRepository<TEntity> where TEntity : class
{
    Task<TEntity> GetAsync(Guid id);
        
    Task<IEnumerable<TEntity>> GetAllAsync();
        
    Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);

    Task CreateAsync(TEntity entity);

    void Update(TEntity entity);

    void Delete(TEntity entity);

    void DeleteRange(IEnumerable<TEntity> entities);
}