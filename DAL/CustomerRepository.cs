using Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace DAL
{
    public class CustomerRepository : IRepository<Customer>
    {
        private UserContext db;

        public CustomerRepository(UserContext context)
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

        public Customer Get(int id)
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
