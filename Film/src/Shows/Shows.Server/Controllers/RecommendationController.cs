using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Shows.Server.Database;

namespace Shows.Server.Controllers
{
    public class RecommendationController : ApiController
    {
        private ShowsDbContext dbContext = new ShowsDbContext();

        public void Post(int userId, int showId, [FromBody] string friendName)
        {
            //todo
        }

        public void Post(int userId, int showId, [FromBody] int groupId)
        {
            //todo
        }
    }
}