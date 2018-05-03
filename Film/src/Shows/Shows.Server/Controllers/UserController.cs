using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Shows.Core.Models;
using Shows.Server.Database;

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
    }
}