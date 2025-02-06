using Core.Models;
using DAL.Data;
using DAL.Repositories;
using FakeItEasy;
using Microsoft.EntityFrameworkCore;

namespace DAL.Test.Repositories
{
    public class ProductRepositoryTest
    {
        private readonly StoreContext _storeContext = A.Fake<StoreContext>();

        [Fact]
        public async Task GetProduct_Success_Test()
        {
            var productId = Guid.NewGuid();
            var fakeProduct = new Product { Id = productId, Name = "Product" };

            _storeContext.Products = A.Fake<DbSet<Product>>();

            A.CallTo(() => _storeContext.Products.FindAsync(A<Guid>._))
                .Returns(new ValueTask<Product>(fakeProduct));

            var productRepository = new ProductRepository(_storeContext);
            var result = await productRepository.GetAsync(productId);

            Assert.NotNull(result);
            Assert.Equal(fakeProduct.Id, result.Id);
            Assert.Equal(fakeProduct.Name, result.Name);

            A.CallTo(() => _storeContext.Products.FindAsync(productId)).MustHaveHappenedOnceExactly();
        }
    }
}
