using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NUnit.Framework;
using Shows.Core.Models;
using Shows.Server.Database;

namespace Shows.Server
{
    [TestFixture]
    public class Tests
    {
        private ShowsDbContext dbContext;

        [SetUp]
        public void SetUp()
        {
            dbContext = new ShowsDbContext();
        }

        [TearDown]
        public void TearDown()
        {
            dbContext.Users.Remove(dbContext.Users.First(x => x.Username == "test_user"));
            dbContext.SaveChanges();
        }

        [Test]
        public void TestAddUser()
        {
            var user = new User()
            {
                PublicId = Guid.NewGuid(),
                Username = "test_user",
                UserLevel = UserLevel.Regular,
                Password = "pwd",
                Groups = new List<UserGroup>(),
                Interests = new List<UserInterest>(),
                Reviews = new List<UserReview>(),
                UserShowHistory = new List<UserShowHistory>()
            };

            Assert.IsNull(dbContext.Users.FirstOrDefault(x => x.Username == "test_user"));

            dbContext.Users.Add(user);
            dbContext.SaveChanges();

            Assert.IsNotNull(dbContext.Users.FirstOrDefault(x => x.Username == "test_user"));
        }
    }
}