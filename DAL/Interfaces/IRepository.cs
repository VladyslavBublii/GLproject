using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T Get(Guid id);
        void Create(T item);
        //IEnumerable<T> Find(Func<T, Boolean> predicate);
        T Find(Guid id);

        void Update(T item);
        void Delete(Guid id);
    }
}
