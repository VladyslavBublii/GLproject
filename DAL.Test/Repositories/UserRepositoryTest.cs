using Core.Models;
using DAL.Data;
using DAL.Repositories;
using FakeItEasy;
using Microsoft.EntityFrameworkCore;

namespace DAL.Test.Repositories
{
    public class UserRepositoryTest
    {
        private readonly StoreContext _storeContext = A.Fake<StoreContext>();

        [Fact]
        public async Task GetUser_Success_Test()
        {
            var userId = Guid.NewGuid();
            var fakeUser = new User { Id = userId, Email = "test@gmail.com" };

            _storeContext.Users = A.Fake<DbSet<User>>();

            A.CallTo(() => _storeContext.Users.FindAsync(A<Guid>._))
                .Returns(new ValueTask<User>(fakeUser));

            var userRepository = new UserRepository(_storeContext);
            var result = await userRepository.GetAsync(userId);

            Assert.NotNull(result);
            Assert.Equal(fakeUser.Id, result.Id);
            Assert.Equal(fakeUser.Email, result.Email);

            A.CallTo(() => _storeContext.Users.FindAsync(userId)).MustHaveHappenedOnceExactly();
        }
    }
}
