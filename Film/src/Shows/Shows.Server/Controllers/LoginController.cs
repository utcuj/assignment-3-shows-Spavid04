using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Shows.Core.Models;
using Shows.Server.Database;

namespace Shows.Server.Controllers
{
    public class LoginController : ApiController
    {
        private ShowsDbContext dbContext = new ShowsDbContext();
        
        public Tuple<Guid,UserLevel> Get(string username, string password)
        {
            var user = dbContext.Users.FirstOrDefault(x => x.Username == username && x.Password == password);

            return user == null ? new Tuple<Guid, UserLevel>(Guid.Empty, 0) : new Tuple<Guid, UserLevel>(user.PublicId, user.UserLevel);
        }
    }
}
