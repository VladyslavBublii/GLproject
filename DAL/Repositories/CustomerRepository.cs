using Core.Models;
using DAL.Data;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace DAL.Repositories
{
    public class CustomerRepository : IRepository<Customer>
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

        public void Delete(int id)
        {
            Customer customer = db.Customers.Find(id);
            if (customer != null)
            {
                db.Customers.Remove(customer);
            }
        }

        public Customer Get(Guid id)
        {
            return db.Customers.Find(id);
        }

        public IEnumerable<Customer> GetAll()
        {
            return db.Customers;
        }

        public void Update(Customer customer)
        {
            db.Entry(customer).State = EntityState.Modified;
        }
    }
}
