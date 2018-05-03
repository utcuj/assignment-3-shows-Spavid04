using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Shows.Core.Models;
using Shows.Server.Database;

namespace Shows.Server.Controllers
{
    public class UserHistoryController : ApiController
    {
        private ShowsDbContext dbContext = new ShowsDbContext();

        public List<UserShowHistory> Get(int userId)
        {
            var history = dbContext.Users.First(x => x.Id == userId).UserShowHistory;

            return history.ToList();
        }

        public void Post([FromBody] Tuple<int, int> userAndShowId)
        {
            var user = dbContext.Users.First(x => x.Id == userAndShowId.Item1);
            var show = dbContext.Shows.First(x => x.Id == userAndShowId.Item2);

            user.UserShowHistory.Add(new UserShowHistory()
            {
                PublicId = Guid.NewGuid(),
                DateTime = DateTime.Now,
                User = user,
                Show = show
            });
            dbContext.SaveChanges();
        }
    }
}