using Core.Models;
using DAL.Data;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Repositories
{
    public class CustomerRepository : IRepository<Customer>, ICustomersRepository
    {
        private DBContext db;

        public CustomerRepository(DBContext context)
        {
            this.db = context;
        }

        public void Create(Customer customer)
        {
            db.Customers.Add(customer);
        }

        public void Delete(Guid id)
        {
            Customer customer = db.Customers.Find(id);
            if (customer != null)
            {
                db.Customers.Remove(customer);
            }
        }

        public void DeleteRange(IEnumerable<Customer> customers)
        {
            db.Customers.RemoveRange(customers);
        }

        public Customer Get(Guid id)
        {
            return db.Customers.Find(id);
        }

        public IEnumerable<Customer> Get(IEnumerable<Guid> ids)
        {
            return db.Customers.Where(c => ids.Contains(c.Id)).ToList();
        }

        public Customer GetByUserId(Guid userId)
        {
            return db.Customers.Where(p => p.UserId == userId).FirstOrDefault();
        }

        public IEnumerable<Customer> GetAll()
        {
            return db.Customers.ToList();
        }

        public void Update(Customer customer)
        {
            db.Entry(customer).State = EntityState.Modified;
        }

        public Customer Find(Guid id)
        {
            var resultData = db.Customers.Where(p => p.Id == id).FirstOrDefault();
            return resultData;
        }
    }
}
