using Core.Models;
using DAL.Data;
using DAL.Repositories;
using FakeItEasy;
using Microsoft.EntityFrameworkCore;

namespace DAL.Test.Repositories
{
    public class ProductRepositoryTest
    {
        private readonly DBContext _db = A.Fake<DBContext>();

        [Fact]
        public void GetProduct_Success_Test()
        {
            _db.Products = A.Fake<DbSet<Product>>();
            var productId = Guid.NewGuid();
            var fakeProduct = new Product { Id = productId, Name = "Product" };
            A.CallTo(() => _db.Products.Find(A<Guid>._))
                .Returns(fakeProduct);
            var productRepository = new ProductRepository(_db);
            var result = productRepository.Get(productId);

            Assert.NotNull(result);
            Assert.Equal(fakeProduct, result);
        }
    }
}
