using Core.Models;
using DAL.Data;
using DAL.Interfaces;
using System;
using System.Collections.Generic;

namespace DAL.Repositories
{
    class NewOrderRepository : IRepository<RightOrder>
    {
        private DBContext _db;

        public NewOrderRepository(DBContext context)
        {
            this._db = context;
        }

        public void Create(RightOrder order)
        {
            _db.RightOrders.Add(order);
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public RightOrder Find(Guid id)
        {
            throw new NotImplementedException();
        }

        public RightOrder Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RightOrder> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(RightOrder item)
        {
            throw new NotImplementedException();
        }
    }
}
