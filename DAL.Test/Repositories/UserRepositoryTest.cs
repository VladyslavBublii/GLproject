using Core.Models;
using DAL.Data;
using DAL.Interfaces;
using DAL.Repositories;
using FakeItEasy;
using Microsoft.Data.SqlClient.Server;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DAL.Test.Repositories
{
    public class UserRepositoryTest
    {
        private readonly DBContext _db = A.Fake<DBContext>();

        [Fact]
        public void GetUser_Success_Test()
        {
            _db.Users = A.Fake<DbSet<User>>();
            var userId = Guid.NewGuid();
            var fakeUser = new User { Id = userId, Email = "test@gmail.com" };
            A.CallTo(() => _db.Users.Find(A<Guid>._))
                .Returns(fakeUser);
            var userRepository = new UserRepository(_db);
            var result = userRepository.Get(userId);

            Assert.NotNull(result);
            Assert.Equal(fakeUser, result);
        }
    }
}
