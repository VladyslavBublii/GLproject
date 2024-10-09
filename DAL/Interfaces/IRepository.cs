using System;
using System.Collections.Generic;

namespace DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T Get(Guid id);
        IEnumerable<T> Get(IEnumerable<Guid> T);
        T Find(Guid id);
        void Create(T item);
        void Update(T item);
        void Delete(Guid id);
        void DeleteRange(IEnumerable<T> collection);
        //IEnumerable<T> Find(Func<T, Boolean> predicate);
    }
}
