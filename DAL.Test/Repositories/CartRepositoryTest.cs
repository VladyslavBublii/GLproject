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
        private readonly DBContext _db = A.Fake<DBContext>();

        [Fact]
        public async Task GetCart_Success_Test()
        {
            _db.Carts = A.Fake<DbSet<Cart>>();
            var cartId = Guid.NewGuid();
            var fakeCart = new Cart { Id = cartId };

            A.CallTo(() => _db.Carts.FindAsync(cartId))
                .Returns(new ValueTask<Cart>(fakeCart));

            var cartRepository = new CartRepository(_db);

            var result = await cartRepository.GetAsync(cartId);

            Assert.NotNull(result);
            Assert.Equal(fakeCart, result);
        }
    }
}
