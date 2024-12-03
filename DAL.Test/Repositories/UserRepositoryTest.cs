using Core.Models;
using DAL.Data;
using DAL.Repositories;
using FakeItEasy;
using Microsoft.EntityFrameworkCore;

namespace DAL.Test.Repositories
{
    public class UserRepositoryTest
    {
        private readonly DBContext _db = A.Fake<DBContext>();

        [Fact]
        public async Task GetUser_Success_Test()
        {
            var userId = Guid.NewGuid();
            var fakeUser = new User { Id = userId, Email = "test@gmail.com" };

            _db.Users = A.Fake<DbSet<User>>();

            A.CallTo(() => _db.Users.FindAsync(A<Guid>._))
                .Returns(new ValueTask<User>(fakeUser));

            var userRepository = new UserRepository(_db);
            var result = await userRepository.GetAsync(userId);

            Assert.NotNull(result);
            Assert.Equal(fakeUser.Id, result.Id);
            Assert.Equal(fakeUser.Email, result.Email);

            A.CallTo(() => _db.Users.FindAsync(userId)).MustHaveHappenedOnceExactly();
        }
    }
}
