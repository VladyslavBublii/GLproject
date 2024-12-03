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
        public async Task GetCustomer_Success_Test()
        {
            var customerId = Guid.NewGuid();
            var fakeCustomer = new Customer { Id = customerId, Name = "Customer" };

            _db.Customers = A.Fake<DbSet<Customer>>();

            A.CallTo(() => _db.Customers.FindAsync(A<Guid>._))
                .Returns(new ValueTask<Customer>(fakeCustomer));

            var customerRepository = new CustomerRepository(_db);
            var result = await customerRepository.GetAsync(customerId);

            Assert.NotNull(result);
            Assert.Equal(fakeCustomer.Id, result.Id);
            Assert.Equal(fakeCustomer.Name, result.Name);

            A.CallTo(() => _db.Customers.FindAsync(customerId)).MustHaveHappenedOnceExactly();
        }
    }
}
