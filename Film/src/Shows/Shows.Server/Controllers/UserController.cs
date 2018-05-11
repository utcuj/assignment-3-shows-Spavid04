using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Http;
using Shows.Core.Models;
using Shows.Server.Database;
using Shows.Server.Filters;

namespace Shows.Server.Controllers
{
    public class UserController : ApiController
    {
        private ShowsDbContext dbContext = new ShowsDbContext();

        public User Get(string username)
        {
            var user = dbContext.Users.FirstOrDefault(x => x.Username == username);

            return user;
        }

        public User Get(Guid guid)
        {
            var user = dbContext.Users.FirstOrDefault(x => x.PublicId == guid);

            return user;
        }

        public List<User> Get(string username, UserLevel level)
        {
            Filter<User> filter = new StandardUserFilter(username, level);

            var users = filter.ApplyFilter(dbContext.Users);

            return users.ToList();
        }

        public void Post([FromBody] User user)
        {
            if (user.PublicId == Guid.Empty)
            {
                //new
                user.PublicId = Guid.NewGuid();
            }
            
            dbContext.Users.AddOrUpdate(user);
            dbContext.SaveChanges();
        }

        public void Delete(int userId)
        {
            var user = dbContext.Users.First(x => x.Id == userId);

            dbContext.UserInterests.RemoveRange(user.Interests);
            dbContext.SaveChanges();

            dbContext.UserReviews.RemoveRange(user.Reviews);
            dbContext.SaveChanges();

            dbContext.UserHistory.RemoveRange(user.UserShowHistory);
            dbContext.SaveChanges();

            foreach (var group in dbContext.UserGroups)
            {
                group.Users.RemoveAll(x => x.Id == userId);
            }
            dbContext.SaveChanges();

            dbContext.Users.Remove(user);
            dbContext.SaveChanges();
        }
    }
}