using Core.Models;
using DAL.Data;
using DAL.Repositories;
using FakeItEasy;
using Microsoft.EntityFrameworkCore;

namespace DAL.Test.Repositories
{
    public class CartRepositoryTest
    {
        private readonly DBContext _db = A.Fake<DBContext>();

        [Fact]
        public void GetCart_Success_Test()
        {
            _db.Carts = A.Fake<DbSet<Cart>>();
            var cartId = Guid.NewGuid();
            var fakeCart = new Cart { Id = cartId };
            A.CallTo(() => _db.Carts.Find(A<Guid>._))
                .Returns(fakeCart);
            var cartRepository = new CartRepository(_db);
            var result = cartRepository.Get(cartId);

            Assert.NotNull(result);
            Assert.Equal(fakeCart, result);
        }
    }
}
