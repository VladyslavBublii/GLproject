using Core.Models;
using DAL.Data;
using DAL.Repositories;
using FakeItEasy;
using Microsoft.EntityFrameworkCore;

namespace DAL.Test.Repositories
{
    public class CustomerRepositoryTest
    {
        private readonly DBContext _db = A.Fake<DBContext>();

        [Fact]
        public void GetCustomer_Success_Test()
        {
            _db.Customers = A.Fake<DbSet<Customer>>();
            var customerId = Guid.NewGuid();
            var fakeCustomer = new Customer { Id = customerId, Name = "Customer" };
            A.CallTo(() => _db.Customers.Find(A<Guid>._))
                .Returns(fakeCustomer);
            var customerRepository = new CustomerRepository(_db);
            var result = customerRepository.Get(customerId);

            Assert.NotNull(result);
            Assert.Equal(fakeCustomer, result);
        }
    }
}
