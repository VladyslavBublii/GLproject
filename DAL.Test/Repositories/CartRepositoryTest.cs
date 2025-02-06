using DAL.Data;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;
using FakeItEasy;
using Core.Models;

namespace DAL.Test.Repositories
{
    public class CartRepositoryTest
    {
        private readonly StoreContext _storeContext = A.Fake<StoreContext>();

        [Fact]
        public async Task GetCart_Success_Test()
        {
            _storeContext.Carts = A.Fake<DbSet<Cart>>();
            var cartId = Guid.NewGuid();
            var fakeCart = new Cart { Id = cartId };

            A.CallTo(() => _storeContext.Carts.FindAsync(cartId))
                .Returns(new ValueTask<Cart>(fakeCart));

            var cartRepository = new CartRepository(_storeContext);

            var result = await cartRepository.GetAsync(cartId);

            Assert.NotNull(result);
            Assert.Equal(fakeCart, result);
        }
    }
}
