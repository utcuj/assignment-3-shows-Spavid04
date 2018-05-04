using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Shows.Core.Models;
using Shows.Server.Database;

namespace Shows.Server.Controllers
{
    public class InterestController : ApiController
    {
        private ShowsDbContext dbContext = new ShowsDbContext();

        public void Post([FromBody] Tuple<int,int> userAndShowId)
        {
            var user = dbContext.Users.First(x => x.Id == userAndShowId.Item1);
            var show = dbContext.Shows.First(x => x.Id == userAndShowId.Item2);

            if (dbContext.UserInterests.Any(x => x.User.Id == user.Id && x.Show.Id == show.Id))
                return;

            dbContext.UserInterests.Add(new UserInterest()
            {
                PublicId = Guid.NewGuid(),
                User = user,
                Show = show
            });
            dbContext.SaveChanges();
        }
    }
}